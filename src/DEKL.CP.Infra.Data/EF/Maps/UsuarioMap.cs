using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            //Table
            ToTable("AspNetUsers");

            //Columns

            Property(e => e.Email)
                .HasMaxLength(256);

            Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(256);

        }
    }
}
