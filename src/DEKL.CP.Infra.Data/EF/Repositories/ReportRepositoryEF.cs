using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities.Filters;
using DEKL.CP.Infra.Data.DTO;
using DEKL.CP.Infra.Data.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<IProviderPhysicalLegalPerson> ProviderReport()
        {
            return _providerRepository.ProviderReport();
        }

        public IEnumerable<IBankTransaction> BankTransactionReport(DateTime StartDate, DateTime EndDate)
        {
            return _bankTransactionRepository.BankTransactionReport(StartDate, EndDate);
        }

        IEnumerable<IAccountToPayRelashionships> IReportRepository.AccountToPayReport(AccountsToPayFilter AccountToPayFilterData)
        {
            return _accountToPayRepository.AccountToPayReport(AccountToPayFilterData);
        }
    }
}