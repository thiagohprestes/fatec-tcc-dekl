using DEKL.CP.Domain.Entities;
using System.Collections.Generic;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IAuditRepository : IRepository<Audit>
    {
        Audit GetAudit(int id);
        IEnumerable<Audit> GetAudits { get; }
    }
}
