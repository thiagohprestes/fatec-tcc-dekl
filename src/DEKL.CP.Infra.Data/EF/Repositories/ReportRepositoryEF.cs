using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities.Filters;
using System;
using System.Collections.Generic;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class ReportRepositoryEF : IReportRepository 
    {
        private readonly IAccountToPayRepository _accountToPayRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IBankTransactionRepository _bankTransactionRepository;

        public ReportRepositoryEF(IAccountToPayRepository accountToPayRepository, 
                                  IProviderRepository providerRepository,
                                  IBankTransactionRepository bankTransactionRepository)
        {
            _accountToPayRepository = accountToPayRepository;
            _providerRepository = providerRepository;
            _bankTransactionRepository = bankTransactionRepository;
        }

        public IEnumerable<IProviderPhysicalLegalPerson> ProviderReport() => _providerRepository.ProviderReport();

        public IEnumerable<IBankTransaction> BankTransactionReport(DateTime startDate, DateTime endDate)
        {
            return _bankTransactionRepository.BankTransactionReport(startDate, endDate);
        }

        IEnumerable<IAccountToPayRelashionships> IReportRepository.AccountToPayReport(AccountsToPayFilter accountToPayFilterData)
        {
            return _accountToPayRepository.AccountToPayReport(accountToPayFilterData);
        }
    }
}