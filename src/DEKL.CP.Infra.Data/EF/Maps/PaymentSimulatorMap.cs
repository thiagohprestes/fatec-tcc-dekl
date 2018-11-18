using System.Data.Entity.ModelConfiguration;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class PaymentSimulatorMap : EntityTypeConfiguration<PaymentSimulator>
    {
        public PaymentSimulatorMap()
        {
            //Table
            ToTable(nameof(PaymentSimulator));

            //Columns
            Property(e => e.PaymentDate);

            Property(e => e.Observations);
        }
    }
}
