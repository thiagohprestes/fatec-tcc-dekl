using System;

namespace DEKL.CP.Domain.Entities
{
    public class SimulaConta : EntityBase
    {
        public double Valor { get; set; }
        public DateTime? DataVencimento { get; set; }
        public string Telefone { get; set; }
        public string NumeroContaCorrente { get; set; }
        public Conta Conta { get; set; }
        public Credor Credor { get; set; }
        public Banco Banco { get; set; }
    }
}
