using System.Collections.Generic;
using System.Linq;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.EF.Context;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class InternalBankAccountRepositoryEF : RepositoryEF<InternalBankAccount>, IInternalBankAccountRepository
    {
        private readonly DEKLCPDataContextEF _ctx;

        public InternalBankAccountRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx) => _ctx = ctx;

        public IEnumerable<BankAgency> BankAgencyesActives => _ctx.BankAgencies.Include(nameof(Bank)).Where(ba => ba.Active);

        public override IEnumerable<InternalBankAccount> Actives =>
            _ctx.InternalBankAccounts.Include(nameof(BankAgency)).AsEnumerable();
    }
}