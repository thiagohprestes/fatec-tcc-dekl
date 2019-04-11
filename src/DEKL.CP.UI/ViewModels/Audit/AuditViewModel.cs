using System;
using System.ComponentModel;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.UI.ViewModels.Audit
{
    public class AuditViewModel
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [DisplayName("Nome usuário")]
        public string ApplicationUserFirstName => $"{ApplicationUser.FirstName} {ApplicationUser.LastName}";

        [DisplayName("Modulo")]
        public string Module { get; set; }

        [ DisplayName("Evento")]
        public string Event { get; set; }

        [DisplayName("Data e hora")]
        public DateTime DateTime { get; set; }
    }
}
