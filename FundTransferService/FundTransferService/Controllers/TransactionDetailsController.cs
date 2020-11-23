using FundTransferService.Models;
using FundTransferService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FundTransferService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionDetailsController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TransactionDetailsController));
        private readonly ITransactionDetailRepository _transactionDetailRepository;
        public TransactionDetailsController(ITransactionDetailRepository transactionDetailRepository)
        {
            _transactionDetailRepository = transactionDetailRepository;
        }
       
        // GET api/<TransactionDetailsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                _log4net.Info("Get TransactionDetails by Id accessed");
                var transaction = _transactionDetailRepository.GetById(id);
                return Ok(transaction);

            }
            catch
            {
                _log4net.Error("Error in Getting Booking Details");
                return new NoContentResult();
            }
        }

        // POST api/<TransactionDetailsController>
        [HttpPost]
        public IActionResult Post([FromBody] TransactionDetails transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _log4net.Info("Transaction Details Getting Added");
                    var _transaction = _transactionDetailRepository.Transfer(transaction);
                    _log4net.Info("amount" + _transaction.Amount);
                    return CreatedAtAction(nameof(Post), new { id = transaction.TransactionId }, transaction);
                }
                return BadRequest();
            }
            catch
            {
                _log4net.Error("Error in Adding Transaction Details");
                return new NoContentResult();
            }
        }
    }
}
