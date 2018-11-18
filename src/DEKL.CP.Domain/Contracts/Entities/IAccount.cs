using System;

namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IAccount
    {
        decimal Value { get; set; }
        decimal? PaidValue { get; set; }
        DateTime? PaymentDate { get; set; }
        DateTime MaturityDate { get; set; } //Vencimento
    }
}
