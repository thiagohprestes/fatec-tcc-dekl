using System.Collections.Generic;
using System.ComponentModel;

namespace DEKL.CP.UI.ViewModels.InternalBankAccount
{
    public class InternalBankAccountViewModel
    {
        public int Id { get; set; }
        [DisplayName("Número da conta")]
        public string Number { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Saldo")]
        public decimal Balance { get; set; } = 0;

        [DisplayName("Agência bancária")]
        public int BankAgencyId { get; set; }

        public virtual Domain.Entities.BankAgency BankAgency { get; set; }

        [DisplayName("Transações bancárias")]
        public virtual ICollection<BankTransactionViewModel> BankTransactions { get; set; }
    }
}
