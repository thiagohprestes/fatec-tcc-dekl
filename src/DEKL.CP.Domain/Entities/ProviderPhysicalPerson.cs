using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.Domain.Entities
{
    public class ProviderPhysicalPerson : Provider, IProviderPhysicalPerson
    {
        public string Name { get; set; }
        public string CPF { get; set; }
    }
}
