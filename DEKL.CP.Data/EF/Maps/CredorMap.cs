using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Data.EF.Maps
{
    public class CredorMap : EntityTypeConfiguration<Credor>
    {
        public CredorMap()
        {
            //Table
            ToTable(nameof(Credor));

            //PK
            HasKey(pk => pk.Id);

            //Columns
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);



            Property(e => e.DataCadastro);

            Property(e => e.DataAlteracao);

            //Relationship
            HasOptional(e => e.Endereco)
                .WithMany()
                .HasForeignKey(e => e.EnderecoId);
        }
    }
}
