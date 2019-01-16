using System;
using System.Collections.Generic;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Enums;

namespace DEKL.CP.Domain.Entities
{
    public class AccountToPay : EntityBase, IAccount
    {
        public decimal Value { get; set; }
        public decimal? PaidValue { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime MaturityDate { get; set; } //Vencimento
        public decimal DailyInterest { get; set; } = 0; //Mora diária
        public decimal Penalty { get; set; } = 0; //Multa
        public bool MonthlyAccount { get; set; } = false;
        public Priority Priority { get; set; }
        public PaymentType PaymentType { get; set; }
        public string DocumentNumber { get; set; }
        public short NumberOfInstallments { get; set; } //Número de parcelas
        public int ProviderId { get; set; }
        public virtual Provider Provider { get; set; }
        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int ModuleId { get; set; } = (int)Enums.Module.AccountToPay;
        public virtual Module Module { get; set; }
        public virtual ICollection<Installment> Installments { get; set; }
        public virtual ICollection<PaymentSimulator> PaymentSimulators { get; set; }
    }
}