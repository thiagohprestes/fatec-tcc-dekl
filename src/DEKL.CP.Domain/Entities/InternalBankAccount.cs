using System.Collections.Generic;
using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.Domain.Entities
{
    public class InternalBankAccount : EntityBase, IBankAccount
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; } = 0; //Saldo
        public int BankAgencyId { get; set; }
        public virtual BankAgency BankAgency { get; set; }
        public virtual ICollection<BankTransaction> BankTransactions { get; set; }
    }
}
