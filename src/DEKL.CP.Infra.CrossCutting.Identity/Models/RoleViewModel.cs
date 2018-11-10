using System.ComponentModel.DataAnnotations;

namespace DEKL.CP.Infra.CrossCutting.Identity.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Nome do Grupo")]
        public string Name { get; set; }
    }
}