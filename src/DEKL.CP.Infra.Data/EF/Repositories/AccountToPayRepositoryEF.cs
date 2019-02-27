using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.DTO;
using DEKL.CP.Infra.Data.EF.Context;
using System.Collections.Generic;
using System.Linq;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class AccountToPayRepositoryEF : RepositoryEF<AccountToPay>, IAccountToPayRepository
    {
        private readonly DEKLCPDataContextEF _ctx;

        public AccountToPayRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx) => _ctx = ctx;

        public IEnumerable<IAccountToPayRelashionships> AccountToPayActivesRelashionships
            => (
                    from atp in _ctx.AccountToPays
                    join p in _ctx.Providers on atp.ProviderId equals p.Id
                    join ppp in _ctx.ProviderPhysicalPersons on p.Id equals ppp.Id into temp
                    from lppp in temp.DefaultIfEmpty()
                    join plp in _ctx.ProviderLegalPersons on p.Id equals plp.Id into temp2
                    from lplp in temp2.DefaultIfEmpty()
                    where atp.Active && p.Active
                    select new AccountToPayDTO
                    {
                         Id = atp.Id,
                         DocumentNumber = atp.DocumentNumber,
                         Provider = p.TypeProvider == Domain.Enums.TypeProvider.PhysicalPerson ? lppp.Name : lplp.CorporateName,
                         PaymentType = atp.PaymentType,
                         Value = atp.Value,
                         Penalty = atp.Penalty,
                         MonthlyAccount = atp.MonthlyAccount,
                         MaturityDate = atp.MaturityDate
                    }

               ).AsEnumerable();
    }
}