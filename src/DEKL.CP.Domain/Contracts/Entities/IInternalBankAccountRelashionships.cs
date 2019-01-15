namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IInternalBankAccountRelashionships
    {
        int Id { get; set; }
        string Number { get; set; }
        string Name { get; set; }
        decimal Balance { get; set; }
        short NumberBankAgency { get; set; }
        string NameBank { get; set; }
    }
}
