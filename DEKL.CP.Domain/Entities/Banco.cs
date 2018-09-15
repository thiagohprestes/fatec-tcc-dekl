namespace DEKL.CP.Domain.Entities
{
    public class Banco : EntityBase
    {
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public double TxChEsp { get; set; }
        public double txEmpr { get; set; }
        public double EncargResp { get; set; }
    }
}
