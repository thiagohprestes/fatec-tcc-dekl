using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.Infra.CrossCutting.Identity.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "O Limite do {0} é de {1} caracteres")]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "O Limite do {0} é de {1} caracteres")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [RegularExpression(@"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$", ErrorMessage = "Número Inválido")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Número Inválido")]
        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string PasswordHash { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(PasswordHash), ErrorMessage = "As senhas não coincidem")]
        [Display(Name = "Confirmação da senha")]
        public string ConfirmPasswordHash { get; set; }
    }
}