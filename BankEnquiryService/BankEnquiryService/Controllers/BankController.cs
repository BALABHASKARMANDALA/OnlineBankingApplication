using BankEnquiryService.Models;
using BankEnquiryService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankEnquiryService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BankController));
        private readonly IBankDetailsRepository _bankDetailsRepository;
        public BankController(IBankDetailsRepository bankDetailsRepository)
        {
            _bankDetailsRepository = bankDetailsRepository;
        }

        // GET: api/<BankDetailsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                _log4net.Info(" Http GET is accesed");
                IEnumerable<BankDetails> banklist = _bankDetailsRepository.GetAll();
                return Ok(banklist);
            }
            catch
            {
                _log4net.Error("Error in Get request");
                return new NoContentResult();
            }
        }

        // GET api/<BankDetailsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                _log4net.Info("Http Get by userid " +id+ " is successfull");
                var details = _bankDetailsRepository.GetAccountDetails(id);
                return new OkObjectResult(details);
            }
            catch
            {
                _log4net.Error("Error in Get by userid:" +id);
                return new NoContentResult();
            }
        }
    }
}
