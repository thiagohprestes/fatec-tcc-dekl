using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Data.EF.Maps
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            //Table
            ToTable(nameof(Usuario));

            //PK
            HasKey(pk => pk.Id);

            //Columns
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.Nome)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();

            Property(e => e.Sobrenome)
           .HasColumnType("varchar")
           .HasMaxLength(50);

            Property(e => e.Email)
                .HasColumnType("varchar")
                .HasMaxLength(80)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UQ_dbo.Usuario.Email") { IsUnique = true })
                    );

            Property(e => e.Senha)
                .HasColumnType("char")
                .HasMaxLength(88)
                .IsRequired();

            Property(e => e.Administrador);

            Property(e => e.Ativo);

            Property(e => e.DataCadastro);

            Property(e => e.DataAlteracao);
        }
    }
}
