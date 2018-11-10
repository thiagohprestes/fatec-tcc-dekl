namespace DEKL.CP.Domain.Entities
{
    public class ContaBancaria : EntityBase
    {
        public Agencia Agencia { get; set; }
        public Banco Banco { get; set; }
        public double Saldo { get; set; }
    }
}
