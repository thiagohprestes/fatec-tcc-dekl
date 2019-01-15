using System.Collections.Generic;
using System.Linq;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.DTO;
using DEKL.CP.Infra.Data.EF.Context;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class InternalBankAccountRepositoryEF : RepositoryEF<InternalBankAccount>, IInternalBankAccountRepository
    {
        private readonly DEKLCPDataContextEF _ctx;

        public InternalBankAccountRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx) => _ctx = ctx;

        public IEnumerable<BankAgency> BankAgencyesActives => _ctx.BankAgencies.Include(nameof(Bank)).Where(ba => ba.Active);

        public IEnumerable<IInternalBankAccountRelashionships> InternalBankAccountRelashionships =>
            (
                from iba in _ctx.InternalBankAccounts
                join ba in _ctx.BankAgencies on iba.BankAgencyId equals ba.Id
                join b in _ctx.Banks on ba.BankId equals b.Id
                where iba.Active && ba.Active && b.Active
                select new InternalBankAccountDTO
                {
                    Id = iba.Id,
                    Name = iba.Name,
                    Number = iba.Number,
                    Balance = iba.Balance,
                    NumberBankAgency = ba.Number,
                    NameBank = b.Name
                }
            ).AsEnumerable();
    }
}