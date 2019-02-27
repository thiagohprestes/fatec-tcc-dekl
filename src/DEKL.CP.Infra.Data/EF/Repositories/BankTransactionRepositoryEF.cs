using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.EF.Context;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class BankTransactionRepositoryEF : RepositoryEF<BankTransaction>, IBankTransactionRepository
    {
        private readonly DEKLCPDataContextEF _ctx;

        public BankTransactionRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx) => _ctx = ctx;
    }
}