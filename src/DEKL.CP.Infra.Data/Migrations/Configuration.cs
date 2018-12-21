using System.Collections.ObjectModel;
using System.Linq;

namespace DEKL.CP.Infra.Data.Migrations
{
    using Domain.Entities;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EF.Context.DEKLCPDataContextEF>
    {
        public Configuration() => AutomaticMigrationsEnabled = false;

        protected override void Seed(EF.Context.DEKLCPDataContextEF context)
        {
            if (!context.States.Any())
            {
                var states = new Collection<State>
            {
                new State { Name = "Acre", Initials = "AC" },
                new State { Name = "Alagoas", Initials = "AL" },
                new State { Name = "Amapá", Initials = "AP" },
                new State { Name = "Amazonas", Initials = "AM" },
                new State { Name = "Bahia", Initials = "BA" },
                new State { Name = "Ceará", Initials = "CE" },
                new State { Name = "Distrito Federal", Initials = "DF" },
                new State { Name = "Espírito Santo", Initials = "ES" },
                new State { Name = "Goiás", Initials = "GO" },
                new State { Name = "Maranhão", Initials = "MA" },
                new State { Name = "Mato Grosso", Initials = "MT" },
                new State { Name = "Mato Grosso do Sul", Initials = "MS" },
                new State { Name = "Minas Gerais", Initials = "MG" },
                new State { Name = "Pará", Initials = "PA" },
                new State { Name = "Paraíba", Initials = "PB" },
                new State { Name = "Paraná", Initials = "PR" },
                new State { Name = "Pernambuco", Initials = "PE" },
                new State { Name = "Piauí", Initials = "PI" },
                new State { Name = "Rio de Janeiro", Initials = "RJ" },
                new State { Name = "Rio Grande do Norte", Initials = "RN" },
                new State { Name = "Rio Grande do Sul", Initials = "RS" },
                new State { Name = "Rondônia", Initials = "RO" },
                new State { Name = "Roraima", Initials = "RR" },
                new State { Name = "Santa Catarina", Initials = "SC" },
                new State { Name = "São Paulo", Initials = "SP" },
                new State { Name = "Sergipe", Initials = "SE" },
                new State { Name = "Tocantins", Initials = "TO" }
            };
                context.States.AddRange(states);
            }
        }
    }
}