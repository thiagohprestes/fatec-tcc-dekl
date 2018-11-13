using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DEKL.CP.Infra.CrossCutting.Identity.ViewModels
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar esse Browser?")]
        public bool RememberBrowser { get; set; }

        [HiddenInput]
        public int UserId { get; set; }
    }
}