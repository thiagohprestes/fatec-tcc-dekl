using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.UI.ViewModels.Conta
{
    public class LoginVM
    {
        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O Limite do {0} é de {1} caracteres")]
        [RegularExpression(@"([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)", ErrorMessage = "Email inválido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A {0} é obrigatória")]
        [StringLength(40, ErrorMessage = "O Limite da {0} é de {1} caracteres")]
        public string Senha { get; set; }

        [StringLength(40, ErrorMessage = "O Limite da {0} é de {1} caracteres")]
        [DisplayName("Confirmação de Senha")]
        public string ConfirmaSenha { get; set; }

        [DisplayName("Permanecer Logado")]
        public bool PermanecerLogado { get; set; }

        public string ReturnURL { get; set; }

    }
}
