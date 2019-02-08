using System;
using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.Domain.Entities
{
    public class Installment : EntityBase, IAccount
    {
        public decimal Value { get; set; }
        public decimal? PaidValue { get; set; } = 0;
        public DateTime MaturityDate { get; set; } //Vencimento
        public DateTime? PaymentDate { get; set; }
        public int AccountToPayId { get; set; }
        public virtual AccountToPay AccountToPay { get; set; }
    }
}
