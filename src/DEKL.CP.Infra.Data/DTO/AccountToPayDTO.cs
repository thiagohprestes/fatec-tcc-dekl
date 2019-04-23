using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Enums;
using System;

namespace DEKL.CP.Infra.Data.DTO
{
    public class AccountToPayDTO : IAccountToPayRelashionships
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public string Provider { get; set; }
        public PaymentType PaymentType { get; set; }
        public Priority Priority { get; set; }
        public decimal Value { get; set; }
        public decimal Penalty { get; set; }
        public decimal DailyInterest { get ; set; }
        public bool MonthlyAccount { get; set; }
        public DateTime MaturityDate { get; set; }
        public bool IsPaid { get; set; }
    }
}