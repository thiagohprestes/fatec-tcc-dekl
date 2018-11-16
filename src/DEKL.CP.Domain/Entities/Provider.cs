namespace DEKL.CP.Domain.Entities
{
    public class Provider : EntityBase
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? AdressId { get; set; }
        public virtual Address Address { get; set; }

    }
}
