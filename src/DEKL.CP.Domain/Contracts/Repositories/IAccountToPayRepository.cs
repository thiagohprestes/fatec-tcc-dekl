using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;
using System.Collections.Generic;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IAccountToPayRepository : IRepository<AccountToPay>
    {
        IEnumerable<IAccountToPayRelashionships> AccountToPayActivesRelashionships { get; }
        IEnumerable<IAccountToPayRelashionships> AccountToPayOpenedRelashionships { get; }
    }
}
