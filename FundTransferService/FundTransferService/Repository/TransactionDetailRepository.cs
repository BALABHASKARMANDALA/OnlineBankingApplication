using FundTransferService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundTransferService.Repository
{
    public class TransactionDetailRepository : ITransactionDetailRepository
    {
        private readonly TransactionDetailDbContext transactionDetailsDbContext;
        public TransactionDetailRepository(TransactionDetailDbContext transactionDetailsDbContext)
        {
            this.transactionDetailsDbContext = transactionDetailsDbContext;
        }
        public IEnumerable<TransactionDetails> GetById(int userid)
        {
            return transactionDetailsDbContext.TransactionDetail.Where(b => b.UserId == userid).ToList<TransactionDetails>();
        }
        public TransactionDetails Transfer(TransactionDetails transaction)
        {
            transactionDetailsDbContext.TransactionDetail.Add(transaction);
            transactionDetailsDbContext.SaveChanges();
            return transaction;
        }
    }
}
