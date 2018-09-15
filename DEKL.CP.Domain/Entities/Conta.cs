using System;

namespace DEKL.CP.Domain.Entities
{
    public class Conta : EntityBase
    {
        public Credor Credor { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public short Nivel { get; set; }
        public string Pagamento { get; set; }
        public short Prioridade { get; set; }
        public int Parcelas { get; set; }
        public DateTime? DataLancamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public int Periodo { get; set; }
        public double Juros { get; set; }
        public double Multa { get; set; }
        public double TotalPago { get; set; }
        public double Total { get; set; }
    }
}
