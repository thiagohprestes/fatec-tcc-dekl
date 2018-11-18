using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using DEKL.CP.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class BankMap : EntityTypeConfiguration<Bank>
    {
        public BankMap()
        {
            //Table
            ToTable(nameof(Bank));

            //Columns
            Property(e => e.Number)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute($"UQ_dbo.{nameof(Bank)}.{nameof(Bank.Number)}") { IsUnique = true })
                );

            Property(e => e.Name)
                .HasMaxLength(60)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute($"UQ_dbo.{nameof(Bank)}.{nameof(Bank.Name)}") { IsUnique = true })
                );
        }
    }
}
