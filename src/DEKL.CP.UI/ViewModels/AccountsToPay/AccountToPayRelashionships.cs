using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Enums;
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

        [DisplayName("Prioridade")]
        public Priority Priority { get; set; }

        [DisplayName("Valor"), DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get; set; }

        [DisplayName("Juros")]
        public decimal Penalty { get; set; }

        [DisplayName("Mora diária")]
        public decimal DailyInterest { get; set; }

        [DisplayName("Conta mensal")]
        public bool MonthlyAccount { get; set; }

        [DisplayName("Data de vencimento"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime MaturityDate { get; set; }

        [DisplayName("Pago")]
        public bool IsPaid { get; set; }
    }

    public class ExportarAccountToPayRelashionships
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Data de vencimento"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime MaturityDate { get; set; }

        [DisplayName("Fornecedor")]
        public string Provider { get; set; }

        [DisplayName("Valor"), DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get; set; }

        [DisplayName("Número do documento")]
        public string DocumentNumber { get; set; }
    }
}
