using System.Collections.Generic;
using System.Web.Mvc;

namespace DEKL.CP.Infra.CrossCutting.Identity.ViewModels
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }

        [HiddenInput]
        public int UserId { get; set; }
    }
}