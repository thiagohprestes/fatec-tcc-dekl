using DEKL.CP.Domain.Enums;
using DEKL.CP.UI.ViewModels.Provider;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;
using System.Collections.Generic;

namespace DEKL.CP.UI.ViewModels.AccountsToPay
{
    public class AccountToPayViewModel : IAccount
    {
        public int Id { get; set; }

        [DisplayName("Valor")]
        public decimal Value { get; set; }

        [DisplayName("Valor pago")]
        public decimal? PaidValue { get; set; }

        [DisplayName("Data de pagamento"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? PaymentDate { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; } = string.Empty;

        [Required, DisplayName("Data de vencimento"), DataType(DataType.Date)]
        public DateTime MaturityDate { get; set; }

        [Required, DisplayName("Mora diária")]
        public decimal DailyInterest { get; set; } = 0;

        [Required, DisplayName("Juros")]
        public decimal Penalty { get; set; } = 0;

        [DisplayName("Conta mensal")]
        public bool MonthlyAccount { get; set; } = false;

        [Required, DisplayName("Prioridade")]
        public Priority Priority { get; set; }
         
        [Required, DisplayName("Tipo de pagamento")]
        public PaymentType PaymentType { get; set; }

        [Required, DisplayName("Número do documento")]
        public string DocumentNumber { get; set; }

        [Required, DisplayName("Parcelas")]
        public short NumberOfInstallments { get; set; }


        [Required, DisplayName("Fornecedor")]
        public int ProviderId { get; set; }

        public virtual ProviderViewModel Provider { get; set; }

        public int ApplicationUserId { get; set; }

        public virtual ICollection<Installment> Installments { get; set; }
    }
}