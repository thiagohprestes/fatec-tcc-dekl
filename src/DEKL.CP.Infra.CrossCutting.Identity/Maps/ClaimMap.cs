using DEKL.CP.Infra.CrossCutting.Identity.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.CrossCutting.Identity.Maps
{
    public class ClaimMap : EntityTypeConfiguration<Claim>
    {
        public ClaimMap()
        {
            //Table
            ToTable("AspNetClaims");

            HasKey(e => e.Id);

            //Columns
            Property(e => e.Id)
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.Name);
        }
    }
}
