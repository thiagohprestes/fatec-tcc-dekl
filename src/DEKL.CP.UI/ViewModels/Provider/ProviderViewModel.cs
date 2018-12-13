using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.UI.ViewModels.Provider
{
    public class ProviderViewModel
    {
        [Required]
        [DisplayName("Telefone")]
        public string PhoneNumber { get; set; }

        [DisplayName("Telefone")]
        public string Email { get; set; }

        public int SelectedSateId { get; set; }
        public IEnumerable<State> State { get; set; }
    }
}
