using DEKL.CP.Infra.CrossCutting.Identity.Context;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DEKL.CP.Infra.CrossCutting.Identity.Models
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, int, ApplicationUserRole>
    {
        private readonly ApplicationDbContext _ctx;

        public ApplicationRoleStore(ApplicationDbContext context)
            : base(context) => _ctx = context;
    }
}
