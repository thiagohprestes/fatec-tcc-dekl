using System.Data.Entity.ModelConfiguration;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class BankTransactionMap : EntityTypeConfiguration<BankTransaction>
    {
        public BankTransactionMap()
        {
            //Table
            ToTable(nameof(BankTransaction));

            //Columns
            Property(e => e.NewBalance)
                .HasPrecision(10, 2); 

            //Relationships
            HasRequired(e => e.InternalBankAccount)
                .WithMany(e => e.BankTransactions)
                .HasForeignKey(e => e.InternalBankAccountId)
                .WillCascadeOnDelete(false);

            HasRequired(e => e.ProviderBankAccount)
                .WithMany(e => e.BankTransactions)
                .HasForeignKey(e => e.ProviderBankAccountId)
                .WillCascadeOnDelete(false);
        }   
    }
}
