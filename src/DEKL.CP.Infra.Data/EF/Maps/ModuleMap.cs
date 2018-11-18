using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class ModuleMap : EntityTypeConfiguration<Module>
    {
        public ModuleMap()
        {
            //Table
            ToTable(nameof(Module));

            //Columns
            Property(e => e.Name)
                .HasMaxLength(60)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute($"UQ_dbo.{nameof(Module)}.{nameof(Module.Name)}") { IsUnique = true })
                );

            Property(e => e.Description)
                .HasMaxLength(80)
                .IsRequired();
        }
    }
}
