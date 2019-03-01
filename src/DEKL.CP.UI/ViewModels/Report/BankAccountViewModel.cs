using System.ComponentModel;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Enums;

namespace DEKL.CP.UI.ViewModels.Report
{
    public class BankAccountViewModel : IBankAccountReport
    {
        [DisplayName("Número da conta")]
        public string Number { get; set; }

        [DisplayName("Agência")]
        public string Agency { get; set; }

        [DisplayName("Saldo")]
        public decimal Balance { get; set; }

        [DisplayName("Tipo da conta")]
        public TypeBankAccount TypeBankAccount  { get; set; }
    }

    public class ExportarBankAccountViewModel
    {
        [DisplayName("Número da conta")]
        public string Number { get; set; }

        [DisplayName("Agência")]
        public string Agency { get; set; }

        [DisplayName("Saldo")]
        public decimal Balance { get; set; }

        [DisplayName("Tipo da conta")]
        public string TypeBankAccount { get; set; }
    }
}
