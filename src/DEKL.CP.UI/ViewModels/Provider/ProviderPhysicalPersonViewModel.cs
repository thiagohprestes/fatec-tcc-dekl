using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.UI.ViewModels.Provider
{
    public class ProviderPhysicalPersonViewModel : ProviderViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        public string CPF { get; set; }
    }
}
