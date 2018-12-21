using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.EF.Context;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class StateRepositoryEF : RepositoryEF<State>, IStateRepository
    {
        private readonly DEKLCPDataContextEF _ctx;

        public StateRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx) => _ctx = ctx;
    }
}
