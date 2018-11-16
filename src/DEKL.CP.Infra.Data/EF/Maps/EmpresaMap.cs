using DEKL.CP.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class EmpresaMap : EntityTypeConfiguration<Empresa>
    {
        public EmpresaMap()
        {
            //Table
            ToTable(nameof(Empresa));

            //Columns
            Property(e => e.Nome)
                .IsRequired();

            Property(e => e.Telefone)
                .HasMaxLength(14);

            //Relationship
            HasOptional(e => e.Address)
                .WithMany()
                .HasForeignKey(e => e.AddressId);
        }
    }
}
