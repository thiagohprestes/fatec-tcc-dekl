using DEKL.CP.Domain.Entities;
using System.Collections.Generic;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IBankAgencyRepository : IRepository<BankAgency>
    {
        IEnumerable<Bank> BanksActives { get; }
    }
}
