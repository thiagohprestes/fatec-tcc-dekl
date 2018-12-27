namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IProviderLegalPerson : IProvider
    {
        string CorporateName { get; set; }
        string CNPJ { get; set; }
        string MunicipalRegistration { get; set; }
        string StateRegistration { get; set; }
    }
}
