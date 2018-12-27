using System.Collections.Generic;
using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.Domain.Entities
{
    public class Bank : EntityBase, IBank
    {
        public short Number { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BankAgency> BankAgencies { get; set; }
    }
}
