using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.UI.ViewModels
{
    public class MeuPerfil
    {
        [Required]
        [StringLength(50, ErrorMessage = "O Limite do {0} é de {1} caracteres")]
        public string Nome { get; set; }

        [StringLength(50, ErrorMessage = "O Limite do {0} é de {1} caracteres")]
        public string Sobrenome { get; set; }

        [ReadOnly(true)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [ReadOnly(true)]
        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
    }
}
