using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;
using System.Collections.Generic;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IInternalBankAccountRepository : IRepository<InternalBankAccount>
    {
        IEnumerable<BankAgency> BankAgenciesActives { get; }

        IEnumerable<IInternalBankAccountRelashionships> InternalBankAccountRelashionships { get; }

        InternalBankAccount InternalBankAccountCaixa { get; }
    }
}