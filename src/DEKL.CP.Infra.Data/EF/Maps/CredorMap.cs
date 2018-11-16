using DEKL.CP.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class CredorMap : EntityTypeConfiguration<Credor>
    {
        public CredorMap()
        {
            //Table
            ToTable(nameof(Credor));

            Property(e => e.Tipo)
                .IsRequired();

            //Relationship
            HasOptional(e => e.Address)
                .WithMany()
                .HasForeignKey(e => e.AddressId);
        }
    }
}
