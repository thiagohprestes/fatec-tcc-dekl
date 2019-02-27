using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities.Filters;
using System.Collections.Generic;
using DEKL.CP.Domain.Enums;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IReportRepository
    {
        IEnumerable<IAccountToPayRelashionships> AccountToPayReport(AccountsToPayFilter AccountToPayFilterData);
        IEnumerable<IProviderPhysicalLegalPerson> ProviderReport();
        IEnumerable<IBankAccountReport> BankAccountReport(TypeBankAccount? typeAccount = null);
    }
}