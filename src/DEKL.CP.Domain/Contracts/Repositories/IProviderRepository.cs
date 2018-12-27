using System.Collections.Generic;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IProviderRepository : IRepository<Provider>
    {
        IEnumerable<IProviderPhysicalLegalPerson> AllActivesProviderPhysicalLegalPerson { get; }

        ProviderPhysicalPerson FindActiveProviderPhysicalPerson(int id);

        ProviderLegalPerson FindActiveProviderLegalPerson(int id);
    }
}
