using DEKL.CP.Domain.Entities;
using DEKL.CP.Domain.Helpers;
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
                new Usuario { Nome = "Thiago", Email = "thiago.prestes@fatec.sp.gov.br", Senha = StringHelpers.Encrypt("12345"), NivelAcesso = 1 },
                new Usuario { Nome = "Lucio", Email = "luciorosa@hotmail.com" , Senha = StringHelpers.Encrypt("54321"), NivelAcesso = 1},
                new Usuario { Nome = "Diego", Email = "diego@gmail.com", Senha = StringHelpers.Encrypt("65432"), NivelAcesso = 1}
            };

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();
        }
    }
}