namespace DEKL.CP.Domain.Entities
{
    public class Empresa : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int? EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
