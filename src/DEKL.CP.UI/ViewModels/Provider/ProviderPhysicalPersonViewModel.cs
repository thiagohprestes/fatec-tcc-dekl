using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.UI.ViewModels.Provider
{
    public class ProviderPhysicalPersonViewModel : ProviderViewModel, IProviderPhysicalLegalPerson
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        public string CPF { get; set; }

        public string NameCorporateName { get; set; }

        public string CPFCNPJ { get; set; }
    }
}
