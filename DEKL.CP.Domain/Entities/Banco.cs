namespace DEKL.CP.Domain.Entities
{
    public class Banco : EntityBase
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public double TaxaChequeEspecial { get; set; }
        public double TaxaEmprestimo { get; set; }
        public double EncargResp { get; set; }
        public int? EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
