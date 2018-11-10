using DEKL.CP.Infra.CrossCutting.Identity.Context;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace DEKL.CP.Infra.CrossCutting.Identity.Configuration
{
    public class ApplicationRoleManager : RoleManager<CustomRole, int>
    {
        public ApplicationRoleManager(IRoleStore<CustomRole, int> roleStore)
            :base(roleStore)
        { }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context) 
            => new ApplicationRoleManager(new RoleStore<CustomRole, int, CustomUserRole>(context.Get<ApplicationDbContext>()));
    }
}
