using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.EF.Context;
using System.Collections.Generic;
using System.Linq;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class AuditRepositoryEF : RepositoryEF<Audit>, IAuditRepository
    {
        private readonly DEKLCPDataContextEF _ctx;

        public AuditRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx) => _ctx = ctx;
        public Audit GetAudit(int id) => _ctx.Audits.Include(nameof(ApplicationUser)).Include(nameof(Module)).FirstOrDefault(a => a.Id == id);
        public IEnumerable<Audit> GetAudits => _ctx.Audits.Include(nameof(ApplicationUser)).Include(nameof(Module)).AsEnumerable();
    }
}
