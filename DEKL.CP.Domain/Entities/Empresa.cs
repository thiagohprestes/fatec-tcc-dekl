namespace DEKL.CP.Domain.Entities
{
    public class Empresa : EntityBase
    {
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
