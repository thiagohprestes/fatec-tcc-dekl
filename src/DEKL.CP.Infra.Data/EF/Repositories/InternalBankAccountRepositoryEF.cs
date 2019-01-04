using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.EF.Context;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class InternalBankAccountRepositoryEF : RepositoryEF<InternalBankAccount>, IInternalBankAccountRepository
    {
        private readonly DEKLCPDataContextEF _ctx;

        public InternalBankAccountRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx) => _ctx = ctx;
    }
}
