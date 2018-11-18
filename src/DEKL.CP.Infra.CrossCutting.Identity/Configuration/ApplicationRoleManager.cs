using DEKL.CP.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNet.Identity;

namespace DEKL.CP.Infra.CrossCutting.Identity.Configuration
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, int>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, int> roleStore)
            :base(roleStore)
        { }
    }
}