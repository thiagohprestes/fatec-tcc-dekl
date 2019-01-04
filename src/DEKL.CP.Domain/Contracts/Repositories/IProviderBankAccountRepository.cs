using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;
using System.Collections.Generic;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IProviderBankAccountRepository : IRepository<ProviderBankAccount>
    {
        IEnumerable<BankAgency> BankAgencyesActives { get; }

        IEnumerable<IProviderBankAccountRelashionships> ProviderBankAccountActivesRelashionships { get; }
    }
}
