using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingClientApplication.Models
{
    public class TransactionDetails
    {
        [Key]
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public double FromAccount { get; set; }
        public double ToAccount { get; set; }
        public double Amount { get; set; }
    }
}
