using System.Data.Entity.ModelConfiguration;
using DEKL.CP.Infra.CrossCutting.Identity.Models;

namespace DEKL.CP.Infra.CrossCutting.Identity.Maps
{
    public class ApplicationUserMap : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            //Table
            ToTable("ApplicationUser");

            //Columns
            Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            Property(e => e.LastName)
                .HasMaxLength(50);

            Ignore(e => e.CurrentClientId);
        }
    }
}
