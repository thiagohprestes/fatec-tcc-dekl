using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.UI.ViewModels.BankTransaction
{
    public class BankTransactionViewModel : IBankTransaction
    {
        public AccountToPay AccountToPay { get; set; }
        public Domain.Entities.InternalBankAccount InternalBankAccount { get; set; }
        public ProviderBankAccount ProviderBankAccount { get; set; }
    }
}
