using System.ComponentModel;

namespace DEKL.CP.Domain.Enums
{
    public enum PaymentType
    {
        [Description("Dinheiro")]
        Money,

        [Description("Tranferência Bancária")]
        BankTransfer,

        [Description("Depósito Bancário")]
        BankDeposit
    }
}
