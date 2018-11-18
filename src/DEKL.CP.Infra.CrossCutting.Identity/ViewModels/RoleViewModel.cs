using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.Infra.CrossCutting.Identity.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "FirstName do Grupo")]
        public string Name { get; set; }
    }
}