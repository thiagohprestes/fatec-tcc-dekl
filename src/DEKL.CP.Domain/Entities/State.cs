using System.Collections.Generic;

namespace DEKL.CP.Domain.Entities
{
    public class State : EntityBase
    {
        public string Name { get; set; }
        public string Initials { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
