using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.EF.Context;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class AuditRepositoryEF : RepositoryEF<Audit>, IAuditRepository
    {
        private readonly DEKLCPDataContextEF _ctx;

        public AuditRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx) => _ctx = ctx;
    }
}
