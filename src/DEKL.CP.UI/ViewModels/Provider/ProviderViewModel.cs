using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.UI.ViewModels.Provider
{
    public class ProviderViewModel
    {
        [Required]
        [DisplayName("Telefone")]
        public string PhoneNumber { get; set; }

        [EmailAddress, DisplayName("E-mail")]
        public string Email { get; set; }

        public AddressViewModel Address { get; set; }
    }
}
