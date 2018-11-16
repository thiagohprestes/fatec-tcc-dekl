using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.EF.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DEKL.CP.Infra.Data.EF
{
    public class DbInitializer : CreateDatabaseIfNotExists<DEKLCPDataContextEF>
    {
        protected override void Seed(DEKLCPDataContextEF context)
        {
            var usuarios = new List<Usuario>
            {
                new Usuario { UserName = "Thiago", Email = "thiago.prestes@fatec.sp.gov.br", PasswordHash = "12345678", AddedDate = DateTime.Now },
                new Usuario { UserName = "Lucio", Email = "luciorosa@hotmail.com" , PasswordHash = "54321", AddedDate = DateTime.Now },
                new Usuario { UserName = "Diego", Email = "diego@gmail.com", PasswordHash = "65432", AddedDate = DateTime.Now }
            };

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();
        }
    }
}