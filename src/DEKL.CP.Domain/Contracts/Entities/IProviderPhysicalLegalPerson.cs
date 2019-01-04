namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IProviderPhysicalLegalPerson : IProvider
    {
        int Id { get; set; }
        string NameCorporateName { get; set; }
        string CPFCNPJ { get; set; }
    }
}
