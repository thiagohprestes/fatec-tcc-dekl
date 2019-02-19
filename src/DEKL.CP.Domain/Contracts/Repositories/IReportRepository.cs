using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities.Filters;
using System;
using System.Collections.Generic;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IReportRepository
    {
        IEnumerable<IAccountToPayRelashionships> AccountToPayReport(AccountsToPayFilter AccountToPayFilterData);
        IEnumerable<IProviderPhysicalLegalPerson> ProviderReport();
        IEnumerable<IBankTransaction> BankTransactionReport(DateTime StartDate, DateTime EndDate);
    }
}