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
    [Route("api/Parking")]
    public class ParkingController : Controller
    {
        private readonly DataService dataService;

        public ParkingController(DataService dataService)
        {
            this.dataService = dataService;
        }

        // GET: api/Parking/free
        [HttpGet("free")]
        public IEnumerable<string> GetFreeParkingPlaces()
        {
            return new string[] { "Free parking places: " + dataService.Menu.GetFreeParkingPlaces().ToString() };
        }

        [HttpGet("occupied")]
        public IEnumerable<string> GetOccupiedParkingPlaces()
        {
            return new string[] { "Occupied parking places: " + dataService.Menu.GetOccupiedParkingPlaces().ToString() };
        }

        [HttpGet("parking_balance")]
        public IEnumerable<string> GetParkingBalance()
        {
            return new string[] { "Parking balance: " + dataService.Menu.GetParkingBalance().ToString() };
        }

        // GET: api/Parking/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Parking
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Parking/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
