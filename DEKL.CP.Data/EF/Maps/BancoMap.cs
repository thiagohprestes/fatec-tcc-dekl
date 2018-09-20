using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Data.EF.Maps
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

       

            Property(e => e.DataCadastro);

            Property(e => e.DataAlteracao);

            //Relationship
            HasRequired(e => e.Endereco)
                .WithRequiredPrincipal();
        }
    }
}
