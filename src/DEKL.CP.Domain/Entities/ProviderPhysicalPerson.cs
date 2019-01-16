using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Enums;

namespace DEKL.CP.Domain.Entities
{
    public class ProviderPhysicalPerson : Provider, IProviderPhysicalPerson
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public override TypeProvider TypeProvider { get; set; } = TypeProvider.PhysicalPerson;
    }
}
