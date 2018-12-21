using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Enums;

namespace DEKL.CP.Infra.Data.DTO
{
    public class ProviderPhysicalLegalPersonDTO : IProviderPhysicalLegalPerson
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NameCorporateName { get; set; }
        public string CPFCNPJ { get; set; }
        public TypeProvider TypeProvider { get; set; }
    }
}
