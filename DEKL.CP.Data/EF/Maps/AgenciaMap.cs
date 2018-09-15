using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Data.EF.Maps
{
    public class AgenciaMap : EntityTypeConfiguration<Agencia>
    {
        public AgenciaMap()
        {
            //Table
            ToTable(nameof(Agencia));

            //PK
            HasKey(pk => pk.Id);

            //Columns
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);



            Property(e => e.DataCadastro);

            Property(e => e.DataAlteracao);
        }   
    }
}
