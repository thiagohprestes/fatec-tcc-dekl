using DEKL.CP.Domain.Enums;

namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IProvider
    {
        string PhoneNumber { get; set; }
        string Email { get; set; }
        TypeProvider TypeProvider { get; set; }
    }
}
