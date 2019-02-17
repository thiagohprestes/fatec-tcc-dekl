using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IBankTransactionRepository : IRepository<BankTransaction>
    {
        IEnumerable<IBankTransaction> BankTransactionReport(DateTime StartDate, DateTime EndDate);
    }
}