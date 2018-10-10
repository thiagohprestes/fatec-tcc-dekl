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
                new Usuario { Nome = "Thiago", Sobrenome = "Prestes", Email = "thiago.prestes@fatec.sp.gov.br", Senha = "12345678".Encrypt(), Administrador = true },
                new Usuario { Nome = "Lucio", Email = "luciorosa@hotmail.com" , Senha = "54321".Encrypt()},
                new Usuario { Nome = "Diego", Email = "diego@gmail.com", Senha = "65432".Encrypt()}
            };

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();
        }
    }
}