using System.Data.Entity.ModelConfiguration;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class AccountToPayMap : EntityTypeConfiguration<AccountToPay>
    {
        public AccountToPayMap()
        {   
            //Table
            ToTable(nameof(AccountToPay));

            //Columns
            Property(e => e.Value)
                .HasColumnType("money");

            Property(e => e.PaidValue)
                .HasColumnType("money");

            Property(e => e.Description)
                .HasMaxLength(60);

            Property(e => e.MaturityDate);

            Property(e => e.DailyInterest);

            Property(e => e.Penalty);

            Property(e => e.MonthlyAccount);

            Property(e => e.Priority);

            Property(e => e.PaymentType);

            Property(e => e.DocumentNumber)
                .HasMaxLength(80)
                .IsRequired();

            Property(e => e.NumberOfInstallments);

            Property(e => e.PaymentDate);

            //Relationships
            HasRequired(e => e.Provider)
                .WithMany(e => e.AccountsToPay)
                .HasForeignKey(e => e.ProviderId)
                .WillCascadeOnDelete(false);
        }
    }
}
