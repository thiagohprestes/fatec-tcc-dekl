using System.Collections.Generic;
using System.ComponentModel;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.ViewModels.BankAgency;

namespace DEKL.CP.UI.ViewModels.Provider
{
    public class ProviderBankAccountViewModel
    {
        [DisplayName("Número da conta")]
        public string Number { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Agência Bancária")]
        public int BankAgencyId { get; set; }

        public BankAgencyViewModel BankAgency { get; set; }

        [DisplayName("Fornecedor")]
        public int? ProviderId { get; set; }

        public virtual ProviderViewModel Provider { get; set; }

        public virtual ICollection<BankTransactionViewModel> BankTransactions { get; set; }
    }
}
