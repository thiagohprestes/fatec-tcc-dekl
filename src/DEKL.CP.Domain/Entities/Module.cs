using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DEKL.CP.Domain.Entities
{
    public class Module : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Audit> Audits { get; set; } = new Collection<Audit>();
    }
}
