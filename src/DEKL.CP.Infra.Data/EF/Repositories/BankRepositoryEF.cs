using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.EF.Context;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class BankRepositoryEF : RepositoryEF<Bank>, IBankRepository
    {
        public BankRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx)
        { }
    }
}
