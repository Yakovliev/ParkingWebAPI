using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingWebAPI.Services;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionsController : Controller
    {
        private readonly DataService dataService;

        public TransactionsController(DataService dataService)
        {
            this.dataService = dataService;
        }

        // GET: api/Transactions/all
        [HttpGet("all")]
        public IEnumerable<string> GetAllTransactionForLastMinute()
        {
            return new string[] { dataService.Menu.GetTransactionsForLastMinute() };
        }

        // GET: api/Transactions/5
        [HttpGet("id/{id}")]
        public IEnumerable<string> GetTransactionsOfCarForLastMinute(string id)
        {
            try
            {
                return new string[] { dataService.Menu.GetTransactionsOfCarForLastMinute(Convert.ToInt32(id)) };
            }
            catch (FormatException)
            {
                return new string[] { "FormatException!" };
            }
        }

        // POST: api/Transactions
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Transactions/5
        [HttpPut("{id}/{amount}")]
        public IEnumerable<string> PutMoney(int id, string amount)
        {
            try
            {
                return new string[] { dataService.Menu.ReplenishCarBalanceById(id, Convert.ToDouble(amount)) };
            }
            catch (FormatException)
            {
                return new string[] { "FormatException!" };
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
