using DEKL.CP.Domain.Enums;
using System;

namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IAccountToPayRelashionships
    {
        int Id { get; set; }
        string DocumentNumber { get; set; }
        string Provider { get; set; }
        PaymentType PaymentType { get; set; }
        Priority Priority { get; set; }
        decimal Value { get; set; }
        decimal Penalty { get; set; }
        decimal DailyInterest { get; set; }
        bool MonthlyAccount { get; set; }
        DateTime MaturityDate { get; set; }
        bool IsPaid { get; set; }
    }
}
