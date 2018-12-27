using System.Collections.Generic;
using System.ComponentModel;
using DEKL.CP.UI.ViewModels.BankAgency;

namespace DEKL.CP.UI.ViewModels.InternalBankAccount
{
    public class InternalBankAccountViewModel
    {
        [DisplayName("Número")]
        public string Number { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Saldo")]
        public decimal Balance { get; set; } = 0;

        [DisplayName("Agência bancária")]
        public int BankAgencyId { get; set; }

        public virtual BankAgencyViewModel BankAgency { get; set; }

        [DisplayName("Transações bancárias")]
        public virtual ICollection<BankTransactionViewModel> BankTransactions { get; set; }
    }
}
