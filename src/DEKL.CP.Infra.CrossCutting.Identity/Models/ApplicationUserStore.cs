using DEKL.CP.Infra.CrossCutting.Identity.Context;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DEKL.CP.Infra.CrossCutting.Identity.Models
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        private readonly ApplicationDbContext _ctx;

        public ApplicationUserStore(ApplicationDbContext context)
            : base(context) => _ctx = context;
    }
}
