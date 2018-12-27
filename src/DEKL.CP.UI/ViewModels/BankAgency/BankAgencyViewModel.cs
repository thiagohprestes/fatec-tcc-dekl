using System.Collections.Generic;
using System.ComponentModel;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.ViewModels.Bank;

namespace DEKL.CP.UI.ViewModels.BankAgency
{
    public class BankAgencyViewModel
    {
        [DisplayName("Número")]
        public short Number { get; set; }

        public AddressViewModel Address { get; set; }

        [DisplayName("Nome do(a) gerente")]
        public string ManagerName { get; set; }

        [DisplayName("Telefone")]
        public string PhoneNumber { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Banco")]
        public int BankId { get; set; }

        public virtual BankViewModel Bank { get; set; }

        public virtual ICollection<Domain.Entities.InternalBankAccount> InternalBankAccounts { get; set; }
        public virtual ICollection<ProviderBankAccount> ProviderBankAccounts { get; set; }
    }
}
