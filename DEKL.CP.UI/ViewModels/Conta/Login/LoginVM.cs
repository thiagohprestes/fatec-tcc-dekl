using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.UI.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O Limite do {0} é de {1} caracteres")]
        [RegularExpression(@"([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)", ErrorMessage = "{0} inválido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(40, ErrorMessage = "A senha contem entre {2} e {1} caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string Password { get; set; }

        [DisplayName("Permanecer Logado")]
        public bool RememberMe { get; set; }

        public string ReturnURL { get; set; }

    }
}
