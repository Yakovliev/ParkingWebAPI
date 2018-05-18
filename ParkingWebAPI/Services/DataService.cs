using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingClassLibrary;

namespace ParkingWebAPI.Services
{
    public class DataService
    {
        public Menu Menu { get; set; }

        private static readonly Lazy<DataService> lazy = new Lazy<DataService>(() => new DataService());

        public static DataService Instance { get { return lazy.Value; } }

        public DataService()
        {
            Menu = Menu.GetMenu();
        }
    }
}
