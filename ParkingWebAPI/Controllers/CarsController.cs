using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingClassLibrary;
using Newtonsoft.Json;
using System.Net.Http;


namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Cars")]
    public class CarsController : Controller
    {
        private HttpClient HttpClient { get; set; }

        private Menu menu;

        public CarsController()
        {
            menu = Menu.GetMenu();

            HttpClient = new HttpClient();
        }

        // GET: api/Cars
        [HttpGet]
        public string Get()
        {
            return menu.GetAllCars();
        }

        // GET: api/Cars/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Cars
        [HttpPost]
        public IEnumerable<string> Post([FromBody] string value)
        {
            menu.AddCar(CarTypes.Bus);

            return new string[] { "Car added " + value };
        }


        // PUT: api/Cars/5
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

    //ЧОМУСЬ ПЕРЕСТАЛО ПРАЦЮВАТИ........
    ////Обхідний маневр, бо більше нічого не виходить
    ////щось не виходить через enum CarTypes призначати тип машини
    //public class CarDetail
    //{
    //    public string carType { get; set; }
    //    //public double startingBalance { get; set; }
    //}
}
