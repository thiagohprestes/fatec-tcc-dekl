using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class BancoMap : EntityTypeConfiguration<Banco>
    {
        public BancoMap()
        {
            //Table
            ToTable(nameof(Banco));

            //PK
            HasKey(e => e.Id);

            //Columns
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.Nome)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(e => e.Telefone)
               .HasColumnType("varchar")
               .HasMaxLength(14);

            Property(e => e.Email)
              .HasColumnType("varchar")
              .HasMaxLength(80);

            Property(e => e.TaxaChequeEspecial)
              .HasColumnType("money")
              .IsRequired();

            Property(e => e.TaxaEmprestimo)
              .HasColumnType("money")
              .IsRequired();

            Property(e => e.DataCadastro);

            Property(e => e.DataAlteracao);

            //Relationship
            HasOptional(e => e.Endereco)
                .WithMany()
                .HasForeignKey(e => e.EnderecoId);
        }
    }
}
