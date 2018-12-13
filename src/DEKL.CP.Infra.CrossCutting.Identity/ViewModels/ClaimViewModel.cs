using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DEKL.CP.Infra.CrossCutting.Identity.ViewModels
{
    public class ClaimViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Nome da funcionalidade")]
        public string Type { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; } = new List<SelectListItem>();

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Valor da Funcionalidade")]
        public string Value { get; set; }

    }
}
