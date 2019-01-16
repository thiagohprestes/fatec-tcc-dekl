using System.ComponentModel;

namespace DEKL.CP.Domain.Enums
{
    public enum Module
    {
        [Description("Contas a Pagar")]
        AccountToPay = 1,

        [Description("Fornecedor")]
        Provider,

        [Description("Conta Bancária Interna")]
        InternalBankAccount,

        [Description("Conta Bancária de Fornecedor")]
        ProviderBankAccount
    }
}
