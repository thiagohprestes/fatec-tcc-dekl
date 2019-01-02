using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IAddress
    {
       string Street { get; set; }
       string Number { get; set; }
       string ZipCode { get; set; }
       string Complement { get; set; }
       string Neighborhood { get; set; }
       string City { get; set; }
       State State { get; set; }
    }
}
