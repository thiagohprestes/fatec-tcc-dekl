using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.EF.Context;
using System;
using System.Linq;
using System.Collections.Generic;
using DEKL.CP.Infra.Data.DTO;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class BankTransactionRepositoryEF : RepositoryEF<BankTransaction>, IBankTransactionRepository
    {
        private readonly DEKLCPDataContextEF _ctx;

        public BankTransactionRepositoryEF(DEKLCPDataContextEF ctx) : base(ctx) => _ctx = ctx;

        public IEnumerable<IBankTransaction> BankTransactionReport(DateTime StartDate, DateTime EndDate)
        {
            var BankTransactions = _ctx.BankTransactions.Select(bt => new BankTransactionDTO
            {
                AccountToPayId = bt.AccountToPayId,
                AddedDate = bt.AddedDate,
                DocumentNumber = "1",
                ModifiedDate = bt.ModifiedDate,
                NewBalance = bt.NewBalance,
                Value = 10
            }).AsEnumerable();

            return BankTransactions;
        }
    }
}