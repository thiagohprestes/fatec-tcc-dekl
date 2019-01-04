using System.Collections.Generic;
using System.Linq;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.DTO;
using DEKL.CP.Infra.Data.EF.Context;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class ProviderBankAccountRepositoryEF : RepositoryEF<ProviderBankAccount>, IProviderBankAccountRepository
    {
        private readonly DEKLCPDataContextEF _ctx;

        public ProviderBankAccountRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx) => _ctx = ctx;

        public IEnumerable<BankAgency> BankAgencyesActives => _ctx.BankAgencies.Include(nameof(Bank)).Where(ba => ba.Active);

        public IEnumerable<IProviderBankAccountRelashionships> ProviderBankAccountActivesRelashionships
            => (
                    from pba in _ctx.ProviderBankAccounts
                    join ba in _ctx.BankAgencies on pba.BankAgencyId equals ba.Id
                    join b in _ctx.Banks on ba.BankId equals b.Id
                    join p in _ctx.Providers on pba.ProviderId equals p.Id
                    join ppp in _ctx.ProviderPhysicalPersons on p.Id equals ppp.Id into temp
                    from lppp in temp.DefaultIfEmpty()
                    join plp in _ctx.ProviderLegalPersons on p.Id equals plp.Id into temp2
                    from lplp in temp2.DefaultIfEmpty()
                    where pba.Active && ba.Active && b.Active && p.Active
                    select new ProviderBankAccountDTO
                    {
                        Id = pba.Id,
                        Number = pba.Number,
                        Name = pba.Name,
                        NumberBankAgency = ba.Number,
                        NameBank = b.Name,
                        NameProvider = string.IsNullOrEmpty(lppp.Name) ? string.Empty : lppp.Name,
                        CorporateNameProvider = string.IsNullOrEmpty(lplp.CorporateName) ? string.Empty : lplp.CorporateName,
                        CPF = string.IsNullOrEmpty(lppp.CPF) ? string.Empty : lppp.CPF,
                        CNPJ = string.IsNullOrEmpty(lplp.CNPJ) ? string.Empty : lplp.CNPJ,
                    }
                ).AsEnumerable();
    }
}
