using System.Linq;
using DEKL.CP.Infra.CrossCutting.Identity.Models;

namespace DEKL.CP.Infra.CrossCutting.Identity.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context.ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new ApplicationUser
                {
                    FirstName = "Adminstrator",
                    Email = "deklcpadm@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = "ABauAHyvumBbxNWKuEXYLgpB0cfhEjtwrknR/WrLrxoOd6fQ0Q9FfZ0KbLzQyEncmw==", //12345678
                    UserName = "deklcpadm@gmail.com"
                });
            }
        }
    }
}
