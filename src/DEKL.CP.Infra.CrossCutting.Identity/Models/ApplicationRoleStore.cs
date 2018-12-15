using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DEKL.CP.Infra.CrossCutting.Identity.Models
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, int, ApplicationUserRole>
    {
        public ApplicationRoleStore(DbContext context) : base(context)
        { }
    }
}
