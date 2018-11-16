namespace DEKL.CP.UI.ViewModels.AccountsToPay
{
    public class AccountsToPayViewModel
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Fornecedor { get; set; }

        public string Tipo { get; set; }

        public string Valor { get; set; }

        public string Juros { get; set; }

        public string Multa { get; set; }

        public string Vencimento { get; set; }

    }
}
