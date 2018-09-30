using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.UI.ViewModels.Conta.TrocarSenha
{
    public class LoginVM
    {
        [Required(ErrorMessage = "A {0} é obrigatória")]
        [StringLength(40, ErrorMessage = "O Limite da {0} é de {1} caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A {0} é obrigatória")]
        [StringLength(40, ErrorMessage = "O Limite da {0} é de {1} caracteres")]
        public string ConfirmaSenha { get; set; }
    }
}
