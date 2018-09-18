using DEKL.CP.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace DEKL.CP.Data.EF
{
    public class DbInitializer : CreateDatabaseIfNotExists<DEKLCPDataContextEF>
    {
        protected override void Seed(DEKLCPDataContextEF context)
        {
            var usuarios = new List<Usuario>
            {
                new Usuario { Nome = "Thiago", Nivel = "Master", Senha = "12345" },
                new Usuario { Nome = "Lucio", Nivel = "Master", Senha = "54321"},
                new Usuario { Nome = "Diego", Nivel = "Master", Senha = "65432"}
            };

            context.SaveChanges();
        }
    }
}