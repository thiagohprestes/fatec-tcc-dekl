using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class ProviderBankAccountMap : EntityTypeConfiguration<ProviderBankAccount>
    {
        public ProviderBankAccountMap()
        {
            //Table
            ToTable(nameof(ProviderBankAccount));

            //Columns
            Property(e => e.Number)
                .HasMaxLength(20)
                .IsRequired();

            Property(e => e.Name)
                .HasMaxLength(80)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute($"UQ_dbo.{nameof(ProviderBankAccount)}.{nameof(ProviderBankAccount.Name)}") { IsUnique = true }
                        )
                );

            //Relationships
            HasRequired(e => e.Provider)
                .WithMany(e => e.ProviderBankAccounts)
                .HasForeignKey(e => e.ProviderId)
                .WillCascadeOnDelete(false);
        }
    }
}
