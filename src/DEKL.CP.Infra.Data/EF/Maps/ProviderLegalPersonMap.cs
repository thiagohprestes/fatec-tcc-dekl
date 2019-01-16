using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class ProviderLegalPersonMap : EntityTypeConfiguration<ProviderLegalPerson>
    {
        public ProviderLegalPersonMap()
        {
            //Table
            ToTable(nameof(ProviderLegalPerson));

            //Columns
            Property(e => e.CorporateName)
                .IsRequired();

            Property(e => e.CNPJ)
                .HasMaxLength(14)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute($"UQ_dbo.{nameof(ProviderLegalPerson)}.{nameof(ProviderLegalPerson.CNPJ)}") { IsUnique = true }
                        )
                    );

            Property(e => e.MunicipalRegistration)
                .HasMaxLength(11);

            Property(e => e.StateRegistration)
                .HasMaxLength(12);
        }
    }
}
