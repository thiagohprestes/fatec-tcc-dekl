namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IProviderPhysicalPerson : IProvider
    {
        string Name { get; set; }
        string CPF { get; set; }
    }
}