
namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IProvider
    {
        int Id { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
    }
}
