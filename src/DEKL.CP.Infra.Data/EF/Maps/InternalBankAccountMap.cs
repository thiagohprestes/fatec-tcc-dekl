using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class InternalBankAccountMap : EntityTypeConfiguration<InternalBankAccount>
    {
        public InternalBankAccountMap()
        {
            //Table
            ToTable(nameof(InternalBankAccount));

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
                        new IndexAttribute($"UQ_dbo.{nameof(InternalBankAccount)}.{nameof(InternalBankAccount.Number)}") { IsUnique = true }
                        )
                );

            Property(e => e.Balance)
                .HasColumnType("money");

            //Relationships
            HasRequired(e => e.BankAgency)
                .WithMany(e => e.InternalBankAccounts)
                .HasForeignKey(e => e.BankAgencyId)
                .WillCascadeOnDelete(false);
        }
    }
}
