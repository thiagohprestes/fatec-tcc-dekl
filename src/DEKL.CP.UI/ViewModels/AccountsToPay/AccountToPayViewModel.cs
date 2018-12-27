using DEKL.CP.Domain.Enums;
using DEKL.CP.UI.ViewModels.Provider;
using System;
using System.ComponentModel;

namespace DEKL.CP.UI.ViewModels.AccountsToPay
{
    public class AccountToPayViewModel
    {
        [DisplayName("Valor")]
        public decimal Value { get; set; }

        [DisplayName("Valor pago")]
        public decimal? PaidValue { get; set; }

        [DisplayName("Data de pagamento")]
        public DateTime? PaymentDate { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; } = string.Empty;

        [DisplayName("Data de vencimento")]
        public DateTime MaturityDate { get; set; }

        [DisplayName("Mora diária")]
        public decimal DailyInterest { get; set; } = 0;

        [DisplayName("Multa")]
        public decimal Penalty { get; set; } = 0;

        [DisplayName("Conta mensal")]
        public bool MonthlyAccount { get; set; } = false;

        [DisplayName("Prioridade")]
        public Priority Priority { get; set; }

        [DisplayName("Tipo de pagamento")]
        public PaymentType PaymentType { get; set; }

        [DisplayName("Número do documento")]
        public string DocumentNumber { get; set; }

        [DisplayName("Número de parcelas")]
        public short NumberOfInstallments { get; set; }

        [DisplayName("Fornecedor")]
        public int ProviderId { get; set; }

        public virtual ProviderViewModel Provider { get; set; }
    }
}
