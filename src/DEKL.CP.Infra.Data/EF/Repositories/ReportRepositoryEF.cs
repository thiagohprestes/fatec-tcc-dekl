using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities.Filters;
using System.Collections.Generic;
using System.Linq;
using DEKL.CP.Domain.Enums;
using DEKL.CP.Infra.Data.DTO;
using DEKL.CP.Infra.Data.EF.Context;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class ReportRepositoryEF : IReportRepository 
    {
        private readonly DEKLCPDataContextEF _ctx;

        public ReportRepositoryEF(DEKLCPDataContextEF ctx) => _ctx = ctx;

        public IEnumerable<IProviderPhysicalLegalPerson> ProviderReport()
            => (
                from p in _ctx.Providers
                join ppp in _ctx.ProviderPhysicalPersons on p.Id equals ppp.Id into temp
                from lppp in temp.DefaultIfEmpty()
                join plp in _ctx.ProviderLegalPersons on p.Id equals plp.Id into temp2
                from lplp in temp2.DefaultIfEmpty()
                where p.Active
                select new ProviderPhysicalLegalPersonDTO
                {
                    Id = p.Id,
                    NameCorporateName = lppp.Name ?? lplp.CorporateName,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    TypeProvider = p.TypeProvider,
                }
            ).AsEnumerable();

        public IEnumerable<IBankAccountReport> BankAccountReport(TypeBankAccount? typeAccount = null)
        {
            switch (typeAccount)
            {
                case TypeBankAccount.Internal:
                    return (
                               from ibk in _ctx.InternalBankAccounts
                               join ba in _ctx.BankAgencies on ibk.BankAgencyId equals ba.Id
                               join b in _ctx.Banks on ba.BankId equals b.Id
                               where ibk.Active && ba.Active && b.Active
                               select new BankAccountReportDTO
                               {
                                   Number = ibk.Number,
                                   Agency = ba.Number + " " + b.Name,
                                   Balance = ibk.Balance,
                                   TypeBankAccount = TypeBankAccount.Internal
                               }
                           ).AsEnumerable();
                case TypeBankAccount.Provider:
                    return (
                                from pba in _ctx.ProviderBankAccounts
                                join ba in _ctx.BankAgencies on pba.BankAgencyId equals ba.Id
                                join b in _ctx.Banks on ba.BankId equals b.Id
                                where pba.Active && ba.Active && b.Active
                                select new BankAccountReportDTO
                                {
                                    Number = pba.Number,
                                    Agency = ba.Number + " " + b.Name,
                                    TypeBankAccount = TypeBankAccount.Provider
                                }
                           ).AsEnumerable();
                default:
                    return (
                                from ibk in _ctx.InternalBankAccounts
                                join ba in _ctx.BankAgencies on ibk.BankAgencyId equals ba.Id
                                join b in _ctx.Banks on ba.BankId equals  b.Id
                                where ibk.Active && ba.Active && b.Active
                                select new BankAccountReportDTO
                                {
                                    Number = ibk.Number,
                                    Agency = ba.Number + " " + b.Name,
                                    Balance = ibk.Balance,
                                    TypeBankAccount = TypeBankAccount.Internal
                                }
                            ).Union
                            (
                                from pba in _ctx.ProviderBankAccounts
                                join ba in _ctx.BankAgencies on pba.BankAgencyId equals ba.Id
                                join b in _ctx.Banks on ba.BankId equals b.Id
                                where pba.Active && ba.Active && b.Active
                                select new BankAccountReportDTO
                                {
                                    Number = pba.Number,
                                    Agency = ba.Number + " " + b.Name,
                                    Balance = 0,
                                    TypeBankAccount = TypeBankAccount.Provider
                                }
                            ).AsEnumerable();
            }
        }

        public IEnumerable<IAccountToPayRelashionships> AccountToPayReport(AccountsToPayFilter AccountToPayFilterData)
            => (
                    from atp in _ctx.AccountToPays
                    join p in _ctx.Providers on atp.ProviderId equals p.Id
                    join ppp in _ctx.ProviderPhysicalPersons on p.Id equals ppp.Id into temp
                    from lppp in temp.DefaultIfEmpty()
                    join plp in _ctx.ProviderLegalPersons on p.Id equals plp.Id into temp2
                    from lplp in temp2.DefaultIfEmpty()
                    where atp.MaturityDate >= AccountToPayFilterData.InitialDate
                          && atp.MaturityDate <= AccountToPayFilterData.FinalDate
                    select new AccountToPayDTO
                    {
                        Id = atp.Id,
                        MaturityDate = atp.MaturityDate,
                        Provider = p.TypeProvider == Domain.Enums.TypeProvider.PhysicalPerson ? lppp.Name : lplp.CorporateName,
                        Value = atp.Value,
                        DocumentNumber = atp.DocumentNumber
                    }
                ).AsEnumerable();
    }
}