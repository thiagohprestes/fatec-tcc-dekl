using System;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.UI.ViewModels.Audit
{
    public class AuditViewModel
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Module { get; set; }
        public string Event { get; set; }
        public DateTime DateTime { get; set; }
    }
}
