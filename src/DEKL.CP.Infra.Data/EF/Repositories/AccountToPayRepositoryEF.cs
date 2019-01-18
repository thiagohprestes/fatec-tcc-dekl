using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.EF.Context;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class AccountToPayRepositoryEF : RepositoryEF<AccountToPay>, IAccountToPayRepository
    {
        private readonly DEKLCPDataContextEF _ctx;

        public AccountToPayRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx) => _ctx = ctx; 
    }
}
