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
        }
    }
}
