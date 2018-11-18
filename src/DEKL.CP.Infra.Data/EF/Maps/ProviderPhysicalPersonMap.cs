using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class ProviderPhysicalPersonMap : EntityTypeConfiguration<ProviderPhysicalPerson>
    {
        public ProviderPhysicalPersonMap()
        {
            //Table
            ToTable(nameof(ProviderPhysicalPersonMap));

            //Collumns
            Property(e => e.Name)
                .IsRequired();

            Property(e => e.CPF)
                .HasMaxLength(11)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute($"UQ_dbo.{nameof(ProviderPhysicalPerson)}.{nameof(ProviderPhysicalPerson.CPF)}") { IsUnique = true }
                    )
                );
        }
    }
}
