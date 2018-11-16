using System.Collections.Generic;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.UI.ViewModels.Provider
{
    public class ProviderViewModel
    {
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string City { get; set; }
        public int SelectedSateId { get; set; }
        public IEnumerable<State> State { get; set; }
    }
}
