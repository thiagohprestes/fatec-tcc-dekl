using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;

namespace DEKL.CP.Infra.CrossCutting.Identity.Configuration
{
    // Validação do Secutiry Stamp para usuário conectado nos clients registrados.
    public static class ApplicationCookieIdentityValidator
    {
        private static async Task<bool> VerifySecurityStampAsync(ApplicationUserManager manager, CookieValidateIdentityContext context)
        {
            var stamp = context.Identity.FindFirstValue("AspNet.Identity.SecurityStamp");
            var stampUser = await manager.GetSecurityStampAsync(context.Identity.GetUserId<int>());
            
            //arrumar
            return true;
        }

        private static Task<bool> VerifyClientIdAsync(ApplicationUser user, CookieValidateIdentityContext context)
        {
            var clientId = context.Identity.FindFirstValue("AspNet.Identity.ClientId");
            if (string.IsNullOrEmpty(clientId) || user.Clients.All(c => c.Id.ToString() != clientId))
                return Task.FromResult(false);

            user.CurrentClientId = clientId;
            return Task.FromResult(true);

        }

        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity(TimeSpan validateInterval, Func<ApplicationUserManager, ApplicationUser, Task<ClaimsIdentity>> regenerateIdentity)
        {
            return async context =>
            {
                var utcNow = context.Options.SystemClock.UtcNow;
                var issuedUtc = context.Properties.IssuedUtc;
                var expired = false;
                if (issuedUtc.HasValue)
                {
                    var t = utcNow.Subtract(issuedUtc.Value);
                    expired = (t > validateInterval);
                }
                if (expired)
                {
                    var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
                    var userId = context.Identity.GetUserId<int>();
                    if (userManager != null && userId != default(int))
                    {
                        var user = await userManager.FindByIdAsync(userId);
                        var reject = true;
                        if (user != null
                            && await VerifySecurityStampAsync(userManager, context)
                            && await VerifyClientIdAsync(user, context))
                        {
                            reject = false;
                            if (regenerateIdentity != null)
                            {
                                var claimsIdentity = await regenerateIdentity(userManager, user);
                                if (claimsIdentity != null)
                                {
                                    context.OwinContext.Authentication.SignIn(claimsIdentity);
                                }
                            }
                        }
                        if (reject)
                        {
                            context.RejectIdentity();
                            context.OwinContext.Authentication.SignOut(context.Options.AuthenticationType);
                        }
                    }
                }
            };
        }
    }
}
