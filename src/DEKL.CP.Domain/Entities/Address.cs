namespace DEKL.CP.Domain.Entities
{ 
    public class Address : EntityBase
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public int? StateId { get; set; }
        public virtual State State { get; set; }
    }
}
