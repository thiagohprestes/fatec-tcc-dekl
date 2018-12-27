using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.UI.ViewModels.BankAgency;

namespace DEKL.CP.UI.ViewModels.Bank
{
    public class BankViewModel : IBank
    {
        public int Id { get; set; }

        [Required, DisplayName("Nome"), MaxLength(60)]
        public string Name { get; set; }

        [Required, DisplayName("Número"), Range(1, 999, ErrorMessage = "O número do banco deve ser entre 1 e 999")]
        public short Number { get; set; }

        public virtual ICollection<BankAgencyViewModel> BankAgencies { get; set; }
    }
}