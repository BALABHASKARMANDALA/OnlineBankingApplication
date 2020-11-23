using BankEnquiryService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankEnquiryService.Repository
{
    public class BankDetailsRepository : IBankDetailsRepository
    {
        private readonly BankDetailDbContext bankDetailsDbContext;
        public BankDetailsRepository(BankDetailDbContext bankDetailsDbContext)
        {
            this.bankDetailsDbContext = bankDetailsDbContext;
        }

        public IEnumerable<BankDetails> GetAll()
        {
            var banklist = bankDetailsDbContext.BankDetail.ToList();
            return banklist;
        }
        public BankDetails GetAccountDetails(int userid)
        {
            return bankDetailsDbContext.BankDetail.FirstOrDefault(b => b.UserId == userid);
        }
    }
}
