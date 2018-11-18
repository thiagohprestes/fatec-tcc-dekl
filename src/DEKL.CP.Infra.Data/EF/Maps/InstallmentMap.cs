using System.Data.Entity.ModelConfiguration;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class InstallmentMap : EntityTypeConfiguration<Installment>
    {
        public InstallmentMap()
        {
            //Table
            ToTable(nameof(Installment));

            //Columns
            Property(e => e.Value)
                .HasColumnType("money");

            Property(e => e.PaidValue)
                .HasColumnType("money");

            Property(e => e.MaturityDate);

            Property(e => e.PaymentDate);

            //Relationships
            HasRequired(e => e.AccountToPay)
                .WithMany(e => e.Installments)
                .HasForeignKey(e => e.AccountToPayId)
                .WillCascadeOnDelete(false);
        } 
    }
}
