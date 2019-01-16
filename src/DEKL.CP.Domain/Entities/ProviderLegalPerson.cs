using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Enums;

namespace DEKL.CP.Domain.Entities
{
    public class ProviderLegalPerson : Provider, IProviderLegalPerson
    {
        public string CorporateName { get; set; }
        public string CNPJ { get; set; }
        public string MunicipalRegistration { get; set; }
        public string StateRegistration { get; set; }
        public override TypeProvider TypeProvider { get; set; } = TypeProvider.LegalPerson;
    }
}