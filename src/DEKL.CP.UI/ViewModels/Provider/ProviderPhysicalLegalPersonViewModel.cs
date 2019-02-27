using System.ComponentModel;
using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.UI.ViewModels.Provider
{
    public class ProviderPhysicalLegalPersonViewModel : ProviderViewModel, IProviderPhysicalLegalPerson
    {
        [DisplayName("Nome/Razão Social")]
        public string NameCorporateName { get; set; }

        [DisplayName("CPF/CNPJ")]
        public string CPFCNPJ { get; set; }
    }

    public class ExportarProviderPhysicalLegalPersonViewModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Nome/Razão Social")]
        public string NameCorporateName { get; set; }

        [DisplayName("Tipo de Fornecedor")]
        public string TypeProvider { get; set; }

        [DisplayName("Telefone")]
        public string PhoneNumber { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }
    }
}