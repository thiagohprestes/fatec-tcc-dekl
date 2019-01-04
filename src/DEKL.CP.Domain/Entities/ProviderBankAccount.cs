using System.Collections.Generic;
using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.Domain.Entities
{
    public class ProviderBankAccount : EntityBase, IBankAccount
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int BankAgencyId { get; set; }
        public BankAgency BankAgency { get; set; }
        public int? ProviderId { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual ICollection<BankTransaction> BankTransactions { get; set; }

    }
}