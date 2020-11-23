using BankEnquiryService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankEnquiryService
{
    public class BankDetailDbContext : DbContext
    {
        public BankDetailDbContext(DbContextOptions<BankDetailDbContext> options) : base(options)
        {
        }
        public virtual DbSet<BankDetails> BankDetail { get; set; }
    }
}
