using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.Infra.CrossCutting.Identity.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}