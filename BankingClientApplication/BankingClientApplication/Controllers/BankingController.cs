using BankingClientApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BankingClientApplication.Controllers
{
    public class BankingController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BankingController));

        // GET: Banking
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");

                return RedirectToAction("Login");

            }
            else
            {
                _log4net.Info("Banklist getting Displayed");

                List<BankDetails> ItemList = new List<BankDetails>();
                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
             
                    using (var response = await client.GetAsync("https://localhost:44383/api/bank"))
                    {       
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ItemList = JsonConvert.DeserializeObject<List<BankDetails>>(apiResponse);
                    }
                }
                return View(ItemList);
            }
        }

        public async Task<IActionResult> TransferFund(int userid)
        {
            _log4net.Info("Transfer fund in progess");
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                BankDetails bank = new BankDetails();
                TransactionDetails transaction = new TransactionDetails();
                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    userid = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                    using (var response = await client.GetAsync("https://localhost:44383/api/bank/" +userid))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        bank = JsonConvert.DeserializeObject<BankDetails>(apiResponse);
                    }
                    transaction.FromAccount = 0;
                    transaction.ToAccount = 0;
                    transaction.UserId = userid;
                    transaction.Amount = 0;
                }
                return View(transaction);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransferFund(TransactionDetails transaction)
        {
            _log4net.Info("Transaction Done");
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                BankDetails b = new BankDetails();
                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    transaction.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

                    using (var response = await client.GetAsync("https://localhost:44383/api/bank/" + transaction.UserId))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        b = JsonConvert.DeserializeObject<BankDetails>(apiResponse);
                    }
                    transaction.FromAccount = b.AccountNo;
                    var content = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync("https://localhost:44395/api/TransactionDetails/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        transaction = JsonConvert.DeserializeObject<TransactionDetails>(apiResponse);
                    }
                }
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> GetTransactionDetails(int id)
        {
            _log4net.Info("Getting Transaction details");
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else 
            {
                List<TransactionDetails> item = new List<TransactionDetails>();
                ViewBag.Username = HttpContext.Session.GetString("UserName");
                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    if (HttpContext.Session.GetInt32("UserId") != null)
                    { 
                        id = Convert.ToInt32(HttpContext.Session.GetInt32("UserId")); 
                    }
                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44395/api/TransactionDetails/" +id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        item = JsonConvert.DeserializeObject<List<TransactionDetails>>(apiResponse);
                    }
                }
                return View(item);
            }
        }
    }
}
