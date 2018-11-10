using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class AgenciaMap : EntityTypeConfiguration<Agencia>
    {
        public AgenciaMap()
        {
            //Table
            ToTable(nameof(Agencia));

            //PK
            HasKey(e => e.Id);

            //Columns
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.Conta)
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired();

            Property(e => e.Saldo)
                .HasColumnType("money")
                .IsRequired();

            Property(e => e.DataCadastro);

            Property(e => e.DataAlteracao);
        }   
    }
}
