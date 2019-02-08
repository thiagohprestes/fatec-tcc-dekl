using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Enums;
using DEKL.CP.UI.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.UI.ViewModels.AccountsToPay
{
    public class AccountToPayRelashionships : IAccountToPayRelashionships
    {
        public int Id { get; set; }

        [DisplayName("Número do documento")]
        public string DocumentNumber { get; set; }

        [DisplayName("Fornecedor")]
        public string Provider { get; set; }

        [DisplayName("Tipo de pagamento")]
        public PaymentType PaymentType { get; set; }

        [DisplayName("Tipo de pagamento")]
        public string PaymentTypeDescription => PaymentType.GetDescription();

        [DisplayName("Valor"), DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get; set; }

        [DisplayName("Juros")]
        public decimal Penalty { get; set; }

        [DisplayName("Conta mensal")]
        public bool MonthlyAccount { get; set; }

        [DisplayName("Data de vencimento"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime MaturityDate { get; set; }
    }
}
