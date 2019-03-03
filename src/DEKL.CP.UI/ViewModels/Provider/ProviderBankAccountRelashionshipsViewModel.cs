using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Enums;
using System.ComponentModel;

namespace DEKL.CP.UI.ViewModels.Provider
{
    public class ProviderBankAccountRelashionshipsViewModel : IProviderBankAccountRelashionships
    {
        public int Id { get; set; }

        [DisplayName("Número da conta")]
        public string Number { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        public short NumberBankAgency { get; set; }

        public string NameBank { get; set; }

        [DisplayName("Agência")]
        public string DescriptionBankAgency => $"{NumberBankAgency} - {NameBank}";
        public string DescriptionAccount => $"{Number} - {NumberBankAgency} - {NameBank}";

        public TypeProvider TypeProvider { get; set; }

        [DisplayName("Fornecedor")]
        public string NameProvider { get; set; }

        [DisplayName("Fornecedor")]
        public string CorporateNameProvider { get; set; }

        [DisplayName("CPF/CNPJ")]
        public string CPF { get; set; }

        [DisplayName("CPF/CNPJ")]
        public string CNPJ { get; set; }
    }
}
