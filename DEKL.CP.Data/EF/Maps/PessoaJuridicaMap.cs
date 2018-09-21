using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Data.EF.Maps
{
    public class PessoaJuridicaMap : EntityTypeConfiguration<PessoaJuridica>
    {
        public PessoaJuridicaMap()
        {
            //Table
            ToTable(nameof(PessoaJuridica));

            //PK
            HasKey(pk => pk.Id);

            //Columns
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.RazaoSocial)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(e => e.NomeFantasia)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            Property(e => e.DataCadastro);

            Property(e => e.DataAlteracao);
        }
    }
}
