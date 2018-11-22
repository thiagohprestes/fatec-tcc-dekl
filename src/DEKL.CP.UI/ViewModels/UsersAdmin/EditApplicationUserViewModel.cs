using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DEKL.CP.UI.ViewModels.UsersAdmin
{
    public class EditApplicationUserViewModel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Nome")]
        public string FirstName { get; set; }

        [DisplayName("Sobrenome")]
        public string LastName { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }

    }
}
