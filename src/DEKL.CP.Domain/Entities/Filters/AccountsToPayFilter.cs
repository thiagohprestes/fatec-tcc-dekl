using System;

namespace DEKL.CP.Domain.Entities.Filters
{
    public class AccountsToPayFilter
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
    }
}