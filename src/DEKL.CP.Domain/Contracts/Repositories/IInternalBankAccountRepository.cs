using DEKL.CP.Domain.Entities;
using System.Collections.Generic;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IInternalBankAccountRepository : IRepository<InternalBankAccount>
    {
        IEnumerable<BankAgency> BankAgencyesActives { get; }
    }
}
