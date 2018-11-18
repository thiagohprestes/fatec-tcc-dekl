using Microsoft.AspNet.Identity.EntityFramework;

namespace DEKL.CP.Infra.CrossCutting.Identity.Models
{
    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
    {
        public ApplicationRole()
        { }

        public ApplicationRole(string name) { Name = name; }
    }
}
