using System.Collections.Generic;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IState
    {
        string Name { get; set; }
        string Initials { get; set; }

        ICollection<Address> Addresses { get; set; }
    }
}
