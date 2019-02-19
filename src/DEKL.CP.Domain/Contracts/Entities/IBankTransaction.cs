using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IBankTransaction
    {
        int AccountToPayId { get; set; }
        string DocumentNumber { get; set; }
        DateTime AddedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        decimal Value { get; set; }
        decimal NewBalance { get; set; }
    }
}
