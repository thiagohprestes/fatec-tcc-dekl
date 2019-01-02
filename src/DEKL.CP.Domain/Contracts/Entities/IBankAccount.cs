using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IBankAccount
    {
        string Number { get; set; }
        string Name { get; set; }
        BankAgency BankAgency { get; set; }
    }
}
