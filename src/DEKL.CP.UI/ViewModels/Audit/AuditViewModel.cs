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
        public string ApplicationUserName => ApplicationUser?.FirstName + " " + ApplicationUser?.LastName;

        public Module Module { get; set; }

        [DisplayName("Modulo")]
        public string ModuleName => Module?.Name;

        [ DisplayName("Evento")]
        public string Event { get; set; }

        [DisplayName("Data e hora")]
        public DateTime DateTime { get; set; }
    }
}
