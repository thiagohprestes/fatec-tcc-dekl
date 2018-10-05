using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Data.EF.Maps
{
    public class SimulaContaMap : EntityTypeConfiguration<SimulaConta>
    {
        public SimulaContaMap()
        {
            //Table
            ToTable(nameof(SimulaConta));

            //PK
            HasKey(e => e.Id);

            //Columns
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.DataCadastro);

            Property(e => e.DataAlteracao);
        }
    }
}
