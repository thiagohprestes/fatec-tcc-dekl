using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IBankTransaction
    {
        AccountToPay AccountToPay { get; set; }
        InternalBankAccount InternalBankAccount { get; set; }
        ProviderBankAccount ProviderBankAccount { get; set; }
    }
}
