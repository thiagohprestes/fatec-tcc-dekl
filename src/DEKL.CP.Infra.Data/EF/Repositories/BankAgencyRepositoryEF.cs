using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.EF.Context;
using System.Collections.Generic;
using System.Linq;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class BankAgencyRepositoryEF : RepositoryEF<BankAgency>, IBankAgencyRepository
    {
        private readonly DEKLCPDataContextEF _ctx;

        public BankAgencyRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx) => _ctx = ctx;

        public IEnumerable<Bank> BanksActives => _ctx.Banks.Where(b => b.Active);
    }
}
