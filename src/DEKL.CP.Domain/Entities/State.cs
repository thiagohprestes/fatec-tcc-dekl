using System.Collections.Generic;
using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.Domain.Entities
{
    public class State : EntityBase, IState
    {
        public string Name { get; set; }
        public string Initials { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
