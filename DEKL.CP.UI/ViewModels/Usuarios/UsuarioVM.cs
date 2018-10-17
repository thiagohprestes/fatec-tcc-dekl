using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.UI.ViewModels
{
    public class UsuarioVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O Limite do {0} é de {1} caracteres")]
        public string Nome { get; set; }

        [StringLength(50, ErrorMessage = "O Limite do {0} é de {1} caracteres")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O Limite do {0} é de {1} caracteres")]
        [RegularExpression(@"([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)", ErrorMessage = "{0} inválido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A {0} é obrigatória")]
        [StringLength(40, ErrorMessage = "A {0} contem entre {2} e {1} caracteres", MinimumLength = 8)]
        [DisplayName("Senha Temporária")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        
        public bool Ativo { get; set; }
        
        public bool Administrador { get; set; }

        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
    }
}
