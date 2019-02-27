using DEKL.CP.Domain.Enums;

namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IBankAccountReport
    {
        string Number { get; set; }
        string Agency { get; set; }
        decimal Balance { get; set; }
        TypeBankAccount TypeBankAccount { get; set; }
    }
}
