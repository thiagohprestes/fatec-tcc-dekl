using DEKL.CP.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class BancoMap : EntityTypeConfiguration<Banco>
    {
        public BancoMap()
        {
            //Table
            ToTable(nameof(Banco));

            //Columns
 
            Property(e => e.Nome)
                .IsRequired();

            Property(e => e.Telefone)
               .HasMaxLength(14);

            Property(e => e.Email)
              .HasMaxLength(80);

            Property(e => e.TaxaChequeEspecial)
              .HasColumnType("money")
              .IsRequired();

            Property(e => e.TaxaEmprestimo)
              .HasColumnType("money")
              .IsRequired();

            //Relationship
            HasOptional(e => e.Address)
                .WithMany()
                .HasForeignKey(e => e.AddressId);
        }
    }
}
