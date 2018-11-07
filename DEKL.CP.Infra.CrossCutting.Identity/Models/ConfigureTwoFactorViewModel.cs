using System.Collections.Generic;

namespace DEKL.CP.Infra.CrossCutting.Identity.Models
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<string> Providers { get; set; }
    }
}