using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Enums;

namespace DEKL.CP.Infra.Data.DTO
{
    public class BankAccountReportDTO : IBankAccountReport
    {
        public string Number { get; set; }
        public string Agency { get; set; }
        public decimal Balance { get; set; } = 0;
        public TypeBankAccount TypeBankAccount { get; set; }
    }
}
