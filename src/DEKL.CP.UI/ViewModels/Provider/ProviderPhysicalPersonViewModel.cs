using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.UI.ViewModels.AccountsToPay;

namespace DEKL.CP.UI.ViewModels.Provider
{
    public class ProviderPhysicalPersonViewModel : ProviderViewModel, IProviderPhysicalPerson
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        public string CPF { get; set; }

        public virtual ICollection<AccountToPayViewModel> AccountsToPay { get; set; }
        public virtual ICollection<ProviderBankAccountViewModel> ProviderBankAccounts { get; set; }
    }
}
