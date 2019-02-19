using DEKL.CP.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEKL.CP.Infra.Data.DTO
{
    public class BankTransactionDTO : IBankTransaction
    {
        public int AccountToPayId { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public decimal Value { get; set; }
        public decimal NewBalance { get; set; }
    }
}