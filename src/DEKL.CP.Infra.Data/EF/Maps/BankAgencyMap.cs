using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class BankAgencyMap : EntityTypeConfiguration<BankAgency>
    {
        public BankAgencyMap()
        {
            //Table
            ToTable(nameof(BankAgency));

            //Columns
            Property(e => e.Number)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute($"UQ_dbo.{nameof(BankAgency)}.{nameof(BankAgency.Number)}") { IsUnique = true })
                );

            Property(e => e.ManagerName)
                .HasMaxLength(80);

            Property(e => e.PhoneNumber)
                .HasMaxLength(20);

            Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute($"UQ_dbo.{nameof(BankAgency)}.{nameof(BankAgency.Email)}") { IsUnique = true })
                );

            //Relationships
            HasRequired(e => e.Bank)
                .WithMany(e => e.BankAgencies)
                .HasForeignKey(e => e.BankId)
                .WillCascadeOnDelete(false);

            HasRequired(e => e.Address)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);
        }
    }
}
