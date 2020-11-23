using BankEnquiryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankEnquiryService.Repository
{
    public interface IBankDetailsRepository
    {
        IEnumerable<BankDetails> GetAll();
        BankDetails GetAccountDetails(int userid);
    }
}
