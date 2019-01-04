using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Enums;

namespace DEKL.CP.Infra.Data.DTO
{
    public class ProviderBankAccountDTO : IProviderBankAccountRelashionships
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public short NumberBankAgency { get; set; }
        public string NameBank { get; set; }
        public TypeProvider TypeProvider { get; set; }
        public string NameProvider { get; set; }
        public string CorporateNameProvider { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
    }
}
