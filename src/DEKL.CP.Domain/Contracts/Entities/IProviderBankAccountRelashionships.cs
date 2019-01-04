using DEKL.CP.Domain.Enums;

namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IProviderBankAccountRelashionships
    {
        int Id { get; set; }
        string Number { get; set; }
        string Name { get; set; }
        short NumberBankAgency { get; set; }
        string NameBank { get; set; }
        TypeProvider TypeProvider { get; set; }
        string NameProvider { get; set; }
        string CorporateNameProvider { get; set; }
        string CPF { get; set; }
        string CNPJ { get; set; }
    }
}