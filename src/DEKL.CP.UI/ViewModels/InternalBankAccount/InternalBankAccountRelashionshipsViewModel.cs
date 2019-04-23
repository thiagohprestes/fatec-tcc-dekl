using DEKL.CP.Domain.Contracts.Entities;
using System.ComponentModel;

namespace DEKL.CP.UI.ViewModels.InternalBankAccount
{
    public class InternalBankAccountRelashionshipsViewModel : IInternalBankAccountRelashionships
    {
        public int Id { get; set; }

        [DisplayName("Número da conta")]
        public string Number { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Saldo")]
        public decimal Balance { get; set; }

        public short NumberBankAgency { get; set; }

        public string NameBank { get; set; }

        [DisplayName("Agência")]
        public string DescriptionBankAgency => $"Agência: {NumberBankAgency} - Banco: {NameBank}";

        public string DescriptionAccount => $"Conta: {Number} - Agência: {NumberBankAgency} - Banco: {NameBank}";
    }
}
