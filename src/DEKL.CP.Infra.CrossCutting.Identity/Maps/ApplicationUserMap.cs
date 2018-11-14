using System.Data.Entity.ModelConfiguration;
using DEKL.CP.Infra.CrossCutting.Identity.Models;

namespace DEKL.CP.Infra.CrossCutting.Identity.Maps
{
    public class ApplicationUserMap : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            //Table
            ToTable("AspNetUsers");

            //Columns
            Property(e => e.Nome)
                .HasMaxLength(50)
                .IsRequired();

            Property(e => e.Sobrenome)
                .HasMaxLength(50);
        }
    }
}
