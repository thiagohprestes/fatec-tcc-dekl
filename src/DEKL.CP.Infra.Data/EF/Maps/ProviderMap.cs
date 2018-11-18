using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class ProviderMap : EntityTypeConfiguration<Provider>
    {
        public ProviderMap()
        {
            //Table
            ToTable(nameof(Provider));

            //Columns
            Property(e => e.PhoneNumber)
                .HasMaxLength(20);

            Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute($"UQ_dbo.{nameof(ProviderPhysicalPerson)}.{nameof(Provider.Email)}") { IsUnique = true }
                    )
                );
        }
    }
}
