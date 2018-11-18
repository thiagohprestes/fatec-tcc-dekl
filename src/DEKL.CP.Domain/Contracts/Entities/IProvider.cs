using System.Collections.Generic;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IProvider
    {
        string PhoneNumber { get; set; }
        string Email { get; set; }
        int AddressId { get; set; }
        Address Address { get; set; }
        ICollection<AccountToPay> AccountsToPay { get; set; }
    }
}
