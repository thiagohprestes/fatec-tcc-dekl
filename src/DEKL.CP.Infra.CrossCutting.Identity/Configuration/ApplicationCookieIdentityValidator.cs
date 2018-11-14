using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Data.Entity.SqlServer.Utilities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DEKL.CP.Infra.CrossCutting.Identity.Configuration
{
    public class ApplicationCookieIdentityValidator
    {
        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser>(
            TimeSpan validateInterval, Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentity)
            where TManager : UserManager<TUser, int>
            where TUser : class, IUser<int> => OnValidateIdentity(validateInterval, regenerateIdentity, id => id.GetUserId<int>());

        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser, TKey>(
            TimeSpan validateInterval, Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentityCallback,
            Func<ClaimsIdentity, TKey> getUserIdCallback)
            where TManager : UserManager<TUser, TKey>
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey> => async context =>
        {
            var currentUtc = DateTimeOffset.UtcNow;
            if (context.Options?.SystemClock != null)
            {
                currentUtc = context.Options.SystemClock.UtcNow;
            }
            var issuedUtc = context.Properties.IssuedUtc;

            // Only validate if enough time has elapsed
            var validate = (issuedUtc == null);
            if (issuedUtc != null)
            {
                var timeElapsed = currentUtc.Subtract(issuedUtc.Value);
                validate = timeElapsed > validateInterval;
            }
            if (validate)
            {
                var manager = context.OwinContext.GetUserManager<TManager>();
                var userId = getUserIdCallback(context.Identity);
                if (manager != null && userId != null)
                {
                    var user = await manager.FindByIdAsync(userId).WithCurrentCulture();
                    var reject = true;
                    // Refresh the identity if the stamp matches, otherwise reject
                    if (user != null && manager.SupportsUserSecurityStamp)
                    {
                        var securityStamp =
                            context.Identity.FindFirstValue(Constants.DefaultSecurityStampClaimType);
                        if (securityStamp == await manager.GetSecurityStampAsync(userId).WithCurrentCulture())
                        {
                            reject = false;
                            // Regenerate fresh claims if possible and resign in
                            if (regenerateIdentityCallback != null)
                            {
                                var identity = await regenerateIdentityCallback.Invoke(manager, user);
                                if (identity != null)
                                {
                                    // here you need to do magic with the context
                                    // should contain previous cookie value if it was set to be persistent. 
                                    var isPersistent = context.Properties.IsPersistent;

                                    // and now you should set this flag on the new cookie
                                    context.OwinContext.Authentication.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
                                }
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
