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
        
        // POST: api/Cars/car_type. Example^ api/Cars/Truck/10
        [HttpPost("{car_type}/{balance}")]
        public IEnumerable<string> AddCar(string car_type, string balance)
        {
            try
            {
                if (car_type == "Truck")
                {
                    dataService.Menu.AddCar(CarTypes.Truck, Convert.ToDouble(balance));
                    return new string[] { "Car added " + car_type };
                }
                else if (car_type == "Bus")
                {
                    dataService.Menu.AddCar(CarTypes.Bus, Convert.ToDouble(balance));
                    return new string[] { "Car added " + car_type };
                }
                else if (car_type == "Motorcycle")
                {
                    dataService.Menu.AddCar(CarTypes.Motorcycle, Convert.ToDouble(balance));
                    return new string[] { "Car added " + car_type };
                }
                else if (car_type == "Passenger")
                {
                    dataService.Menu.AddCar(CarTypes.Passenger, Convert.ToDouble(balance));
                    return new string[] { "Car added " + car_type };
                }

                return new string[] { "Something went wrong!" };
            }
            catch (FormatException)
            {
                return new string[] { "FormatException!" };
            }


        }


        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IEnumerable<string> DeleteCar(int id)
        {
            return new string[] { dataService.Menu.DeleteCarById(id) };             
        }
    }
}
