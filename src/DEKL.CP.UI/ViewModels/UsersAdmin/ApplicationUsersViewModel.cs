using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.UI.ViewModels.UsersAdmin
{
    public class ApplicationUsersViewModel
    {
        public int Id { get; set; }

        public string FirstName { private get; set; }

        public string LastName { private get; set; }

        [DisplayName("Nome")]
        public string FullName => $"{FirstName} {LastName}";

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayFormat(NullDisplayText = "Não Cadastrado")]
        [DisplayName("Telefone")]
        public string PhoneNumber { get; set; }

        [DisplayName("Confirmou o E-mail")]
        public bool EmailConfirmed { get; set; }
    }
}
