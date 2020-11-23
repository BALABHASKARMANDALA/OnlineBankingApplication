using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankEnquiryService.Models
{
    public class BankDetails
    {
        [Key]
        public int UserId { get; set; }
        public int AccountNo { get; set; }
        public string UserName { get; set; }
        public double Balance { get; set; }
    }
}
