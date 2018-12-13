using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DEKL.CP.UI.ViewModels
{
    public class AddressViewModel
    {
        [Required]
        [DisplayName("Logradouro")]
        public string Street { get; set; }

        [DisplayName("Número")]
        public string Number { get; set; }

        [Required]
        [DisplayName("CEP")]
        public string ZipCode { get; set; }

        [DisplayName("Complemento")]
        public string Complement { get; set; }

        [Required]
        [DisplayName("Bairro")]
        public string Neighborhood { get; set; }

        [Required]
        [DisplayName("Cidade")]
        public string City { get; set; }

        [Required]
        [DisplayName("Estado")]
        public IEnumerable<SelectListItem> State { get; set; }
    }
}
