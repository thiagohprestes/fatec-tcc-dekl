using System.Collections.Generic;

namespace DEKL.CP.Domain.Entities
{
    public class BankAgency : EntityBase
    {
        public short Number { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string ManagerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual ICollection<InternalBankAccount> InternalBankAccounts { get; set; }
        public virtual ICollection<ProviderBankAccount> ProviderBankAccounts { get; set; }
    }
}