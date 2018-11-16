using System.Collections.Generic;

namespace DEKL.CP.UI.ViewModels.AccountsToPay
{
    public class AccountToPayViewModel
    {
        public string Documento { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public string Juros { get; set; }
        public string Mora { get; set; }
        public string Multa { get; set; }
        public string Vencimento { get; set; }
        public int Parcelas { get; set; }
        public int Prazo { get; set; }
        public bool Mensal { get; set; }
        public string Descricao { get; set; }
        public int SelectedProviderId { get; set; }
        public IEnumerable<Domain.Entities.Provider> Providers { get; set; }
        public IEnumerable<string> Prioridade { get; set; } = new List<string> { "Normal", "Alta", "Baixa"};
    }
}
