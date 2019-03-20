using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.UI.ViewModels.Address
{
    public class AddressViewModel : IAddress
    {
        public int Id { get; set; }

        [Required, DisplayName("Logradouro")]
        public string Street { get; set; }

        [Required, DisplayName("Número")]
        public string Number { get; set; }

        [Required, DisplayName("CEP")]
        public string ZipCode { get; set; }

        [DisplayName("Complemento")]
        public string Complement { get; set; }

        [Required,  DisplayName("Bairro")]
        public string Neighborhood { get; set; }
        
        [Required, DisplayName("Cidade")]
        public string City { get; set; }
        
        [Required, DisplayName("Estado")]
        public int? StateId { get; set; }

        public State State { get; set; }
    }
}