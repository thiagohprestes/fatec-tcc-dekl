using System.Collections.Generic;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Enums;

namespace DEKL.CP.Domain.Entities
{
    public class Provider : EntityBase, IProvider
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual TypeProvider TypeProvider { get; set; }
        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int ModuleId { get; set; } = (int)Enums.Module.Provider;
        public virtual Module Module { get; set; }
        public virtual ICollection<AccountToPay> AccountsToPay { get; set; }
        public virtual ICollection<ProviderBankAccount> ProviderBankAccounts { get; set; }
    }
}