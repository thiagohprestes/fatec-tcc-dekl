using System.Collections.Generic;
using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.Domain.Entities
{
    public class BankAgency : EntityBase, IBankAgency
    {
        public short Number { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public string ManagerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual ICollection<InternalBankAccount> InternalBankAccounts { get; set; }
        public virtual ICollection<ProviderBankAccount> ProviderBankAccounts { get; set; }

        public string BankAgencyDescription => $"{Number} - {Bank.Name}";
    }
}