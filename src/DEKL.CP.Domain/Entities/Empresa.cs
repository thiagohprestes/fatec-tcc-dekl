namespace DEKL.CP.Domain.Entities
{
    public class Empresa : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
