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
}