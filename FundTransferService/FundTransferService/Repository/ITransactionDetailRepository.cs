using FundTransferService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundTransferService.Repository
{
    public interface ITransactionDetailRepository
    {
        public IEnumerable<TransactionDetails> GetById(int userid);
        TransactionDetails Transfer(TransactionDetails transaction);
    }
}
