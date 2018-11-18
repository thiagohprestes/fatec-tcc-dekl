namespace DEKL.CP.Domain.Entities
{
    public class BankTransaction : EntityBase
    {
        public int AccountToPayId { get; set; }
        public virtual AccountToPay AccountToPay { get; set; }
        public int InternalBankAccountId { get; set; }
        public virtual InternalBankAccount InternalBankAccount { get; set; }
        public int ProviderBankAccountId { get; set; }
        public virtual ProviderBankAccount ProviderBankAccount { get; set; }
        public decimal NewBalance { get; set; } //Novo Saldo
    }
}
