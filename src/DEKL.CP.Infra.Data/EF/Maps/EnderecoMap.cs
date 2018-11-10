using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class EnderecoMap : EntityTypeConfiguration<Endereco>
    {
        public EnderecoMap()
        {
            //Table
            ToTable(nameof(Endereco));

            //PK
            HasKey(e => e.Id);

            //Columns
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.Logradouro)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(e => e.CEP)
                .HasColumnType("varchar")
                .HasMaxLength(8);

            Property(e => e.Numero);

            Property(e => e.Complemento)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            Property(e => e.Bairro)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(e => e.Cidade)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(e => e.UF)
                .HasColumnType("char")
                .HasMaxLength(2)
                .IsRequired();

            Property(e => e.DataCadastro);

            Property(e => e.DataAlteracao);
        }
    }
}
