using System;
using System.Collections.Generic;

namespace DEKL.CP.Domain.Entities
{
    public class PaymentSimulator : EntityBase
    {
        public DateTime PaymentDate { get; set; }
        public int InternalBankAccountId { get; set; }
        public virtual InternalBankAccount InternalBankAccount { get; set; }
        public virtual ICollection<AccountToPay> AccountsToPay { get; set; } = new List<AccountToPay>();
        public string Observations { get; set; }
    }
}