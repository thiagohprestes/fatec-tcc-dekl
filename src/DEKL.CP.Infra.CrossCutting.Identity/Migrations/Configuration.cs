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
            if (!context.Roles.Any())
            {
                context.Roles.AddOrUpdate(
                    new ApplicationRole("Administrador"),
                    new ApplicationRole("Operacional")
                );
            }

            if (!context.Users.Any())
            {
                context.Users.AddOrUpdate(
       new ApplicationUser
                    {
                        FirstName = "Administrador",
                        Email = "deklcpadm@gmail.com",
                        EmailConfirmed = true,
                        PasswordHash = "AEg0y2CC0+jHho2KiRR+FjJ4SRYRViYTGQC9AYhVz1FEAp8dekcQ5mbYGKpnrzDzHQ==",
                        SecurityStamp = "785d8f39-7179-409e-a02a-9acdf2b94d76",
                        PhoneNumber = "15998233579",
                        TwoFactorEnabled = false,
                        LockoutEnabled = true,
                        UserName = "deklcpadm@gmail.com"
                    },
                    new ApplicationUser
                    {
                        FirstName = "Thiago",
                        LastName = "Prestes",
                        Email = "thiagohenriqueprestes@gmail.com",
                        EmailConfirmed = true,
                        PasswordHash = "AEg0y2CC0+jHho2KiRR+FjJ4SRYRViYTGQC9AYhVz1FEAp8dekcQ5mbYGKpnrzDzHQ==",
                        SecurityStamp = "785d8f39-7179-409e-a02a-9acdf2b94d76",
                        PhoneNumber = "15991925150",
                        TwoFactorEnabled = false,
                        LockoutEnabled = true,
                        UserName = "thiagohenriqueprestes@gmail.com"
                    }
                );
            }

            if (!context.ApplicationUserRole.Any())
            {
                context.ApplicationUserRole.Add(new ApplicationUserRole { RoleId = 1, UserId = 1 });
                context.ApplicationUserRole.Add(new ApplicationUserRole { RoleId = 2, UserId = 2 });
            }
        }
    }
}
