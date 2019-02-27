using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.UI.ViewModels.Provider
{
    public class ProviderBankAccountViewModel : IBankAccount
    {
        public int Id { get; set; }

        [Required, DisplayName("Número da conta")]
        public string Number { get; set; }

        [Required, DisplayName("Nome")]
        public string Name { get; set; }

        [Required, DisplayName("Agência Bancária")]
        public int BankAgencyId { get; set; }

        public Domain.Entities.BankAgency BankAgency { get; set; }

        [Required, DisplayName("Fornecedor")]
        public int? ProviderId { get; set; }

        public virtual Domain.Entities.Provider Provider { get; set; }

        public virtual ICollection<Domain.Entities.BankTransaction> BankTransactions { get; set; }
    }
}
