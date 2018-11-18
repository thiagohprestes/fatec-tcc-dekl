using DEKL.CP.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            //Table
            ToTable(nameof(Address));

            //Columns
            Property(e => e.Street)
                .HasMaxLength(60)
                .IsRequired();

            Property(e => e.Number)
                .HasMaxLength(20);

            Property(e => e.ZipCode)
                .IsRequired();

            Property(e => e.Complement)
                .IsRequired();

            Property(e => e.Neighborhood)
                .HasMaxLength(60)
                .IsRequired();

            Property(e => e.City)
                .HasMaxLength(60)
                .IsRequired();

            //Relationships
            HasRequired(e => e.State)
                .WithMany(e => e.Addresses)
                .HasForeignKey(e => e.StateId)
                .WillCascadeOnDelete(false);
                
        }
    }
}
