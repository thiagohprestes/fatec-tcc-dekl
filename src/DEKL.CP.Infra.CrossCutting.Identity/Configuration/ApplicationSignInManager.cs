using DEKL.CP.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace DEKL.CP.Infra.CrossCutting.Identity.Configuration
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        { }
    }
}