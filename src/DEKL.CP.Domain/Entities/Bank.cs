using System.Collections.Generic;

namespace DEKL.CP.Domain.Entities
{
    public class Bank : EntityBase
    {
        public short Number { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BankAgency> BankAgencies { get; set; }
    }
}
