using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.Infra.Data.DTO
{
    public class InternalBankAccountDTO : IInternalBankAccountRelashionships
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public short NumberBankAgency { get; set; }
        public string NameBank { get; set; }
    }
}
