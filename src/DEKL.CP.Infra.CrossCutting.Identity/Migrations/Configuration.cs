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

            if (!context.Claims.Any())
            {
                context.Claims.AddOrUpdate(
                    new Claim { Name = "Cadastrar Conta a Pagar" },
                    new Claim { Name = "Cadastrar Fornecedor" },
                    new Claim { Name = "Cadastrar Conta Bancária de Fornecedor" },
                    new Claim { Name = "Cadastrar Conta Bancária Interna" }
                );
            }

            if (!context.Users.Any())
            {
                context.Users.AddOrUpdate(new ApplicationUser
                {
                    FirstName = "Administrador",
                    UserName = "admin@dekl.com.br",
                    Email = "admin@dekl.com.br",
                    EmailConfirmed = true,
                    PasswordHash = "ANldua3YrngoW/I6Y+uQMaDsA9hpF448hQKd6+oLryvt6CyYe1U3HIXBpJRD336k4g==", //12345678
                    PhoneNumber = "15998233579",
                    PhoneNumberConfirmed = true
                });
            }
        }
    }
}