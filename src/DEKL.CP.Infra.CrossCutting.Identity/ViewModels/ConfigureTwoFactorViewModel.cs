using System.Collections.Generic;

namespace DEKL.CP.Infra.CrossCutting.Identity.ViewModels
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<string> Providers { get; set; }
    }
}