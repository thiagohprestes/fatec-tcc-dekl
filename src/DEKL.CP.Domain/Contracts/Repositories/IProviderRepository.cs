using System.Collections.Generic;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;
using System;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IProviderRepository : IRepository<Provider>
    {
        IEnumerable<IProviderPhysicalLegalPerson> AllActivesProviderPhysicalLegalPerson { get; }

        IProviderPhysicalLegalPerson ActiveProviderPhysicalLegalPerson(int id);

        ProviderPhysicalPerson FindActiveProviderPhysicalPerson(int id);

        ProviderLegalPerson FindActiveProviderLegalPerson(int id);
        IEnumerable<IProviderPhysicalLegalPerson> ProviderReport();
    }
}
