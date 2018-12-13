using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DEKL.CP.UI.ViewModels
{
    public class TrocaSenhaVM
    {
        [Required]
        [StringLength(40, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres", MinimumLength = 8)]
        [DisplayName("Confirmação de Senha")]
        [Compare("Senha")]
        [DataType(DataType.Password)]
        public string ConfirmaSenha { get; set; }
    }
}
