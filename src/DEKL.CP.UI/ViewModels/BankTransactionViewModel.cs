using System.ComponentModel;
using DEKL.CP.UI.ViewModels.AccountsToPay;
using DEKL.CP.UI.ViewModels.InternalBankAccount;
using DEKL.CP.UI.ViewModels.Provider;

namespace DEKL.CP.UI.ViewModels
{
    public class BankTransactionViewModel
    {
        [DisplayName("Conta a pagar")]
        public int AccountToPayId { get; set; }

        public virtual AccountToPayViewModel AccountToPay { get; set; }

        [DisplayName("Conta bancária Interna")]
        public int InternalBankAccountId { get; set; }

        public virtual InternalBankAccountViewModel InternalBankAccount { get; set; }

        [DisplayName("Conta bancária Fornecedor")]
        public int ProviderBankAccountId { get; set; }

        public virtual ProviderBankAccountViewModel ProviderBankAccount { get; set; }

        [DisplayName("Novo saldo")]
        public decimal NewBalance { get; set; }
    }

    public class ExportBankTransactionViewModel
    {

    }
}
