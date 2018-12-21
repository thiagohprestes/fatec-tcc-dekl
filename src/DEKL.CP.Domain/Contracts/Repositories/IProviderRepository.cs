using System.Collections.Generic;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IProviderRepository : IRepository<Provider>
    {
        IEnumerable<IProviderPhysicalLegalPerson> AllActivesProviderPhysicalLegalPerson { get; }

        void AddProviderPhysicalPerson(ProviderPhysicalPerson providerPhysicalPerson);

        void AddProviderLegalPerson(ProviderLegalPerson providerLegalPerson);
    }
}
