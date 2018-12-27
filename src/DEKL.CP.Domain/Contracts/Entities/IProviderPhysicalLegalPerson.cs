namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IProviderPhysicalLegalPerson : IProvider
    {
        string NameCorporateName { get; set; }
        string CPFCNPJ { get; set; }
    }
}
