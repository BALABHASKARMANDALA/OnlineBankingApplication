using FundTransferService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundTransferService
{
    public class TransactionDetailDbContext : DbContext
    {
        public TransactionDetailDbContext(DbContextOptions<TransactionDetailDbContext> options) : base(options)
        {
        }
        public virtual DbSet<TransactionDetails> TransactionDetail { get; set; }
    }
}
