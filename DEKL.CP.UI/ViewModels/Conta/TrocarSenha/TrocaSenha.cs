using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DEKL.CP.UI.ViewModels
{
    public class TrocaSenhaVM
    {
        public string Email { get; } = HttpContext.Current.User.Identity.Name;

        [Required(ErrorMessage = "A {0} é obrigatória")]
        [StringLength(40, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A {0} é obrigatória")]
        [StringLength(40, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres", MinimumLength = 8)]
        [DisplayName("Confirmação de Senha")]
        [Compare("Senha")]
        public string ConfirmaSenha { get; set; }
    }
}
