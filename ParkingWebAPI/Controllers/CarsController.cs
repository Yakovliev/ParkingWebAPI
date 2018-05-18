using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingClassLibrary;
using Newtonsoft.Json;
using System.Net.Http;
using ParkingWebAPI.Services;


namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Cars")]
    public class CarsController : Controller
    {
        private readonly DataService dataService;

        public CarsController(DataService dataService)
        {
            this.dataService = dataService;
        }

        // GET: api/Cars
        [HttpGet]
        public string GetAllCars()
        {
            return dataService.Menu.GetAllCars();
        }

        // GET: api/Cars/id. Example: api/Cars/3
        [HttpGet("{id}")]
        public string GetCarById(int id)
        {            
            return dataService.Menu.GetCarDetails(id);
        }
        
        // POST: api/Cars/car_type. Example^ api/Cars/Truck
        [HttpPost("{car_type}")]
        public IEnumerable<string> AddCar(string car_type)
        {
            if (car_type == "Truck")
            {
                dataService.Menu.AddCar(CarTypes.Truck);
                return new string[] { "Car added " + car_type };
            }
            else if (car_type == "Bus")
            {
                dataService.Menu.AddCar(CarTypes.Bus);
                return new string[] { "Car added " + car_type };
            }
            else if (car_type == "Motorcycle")
            {
                dataService.Menu.AddCar(CarTypes.Motorcycle);
                return new string[] { "Car added " + car_type };
            }
            else if (car_type == "Passenger")
            {
                dataService.Menu.AddCar(CarTypes.Passenger);
                return new string[] { "Car added " + car_type };
            }

            return new string[] { "Something went wrong!" };

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
            //menu.DeleteCarById(id);
        }
    }

    //ЧОМУСЬ ПЕРЕСТАЛО ПРАЦЮВАТИ........
    ////Обхідний маневр, бо більше нічого не виходить
    ////щось не виходить через enum CarTypes призначати тип машини
    public class CarDetail
    {
        public string carType { get; set; }
        //public double startingBalance { get; set; }
    }
}
