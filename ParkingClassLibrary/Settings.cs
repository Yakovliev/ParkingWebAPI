using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingClassLibrary
{
    public class Settings
    {
        /// <summary>
        /// Timeout - is number of seconds. Every Timeout-seconds write-offs funds for a parking space.
        /// </summary>
        public int Timeout { get; private set; } = 3;

        /// <summary>
        /// Parking prices.
        /// </summary>
        private Dictionary<CarTypes, double> parkingPrices = new Dictionary<CarTypes, double>()
        {
            {CarTypes.Bus, 4D },
            {CarTypes.Truck, 5D },
            {CarTypes.Passenger, 3D },
            {CarTypes.Motorcycle, 2D },
        };

        /// <summary>
        /// Get parking prices.
        /// </summary>
        public Dictionary<CarTypes, double> ParkingPrices { get { return parkingPrices; } }

        /// <summary>
        /// Parking space.
        /// </summary>
        public int ParkingSpace { get; private set; } = 10;

        /// <summary>
        /// Coefficient of fine
        /// </summary>
        public double Fine { get; private set; } = 2d;

        /// <summary>
        /// Instance of Settings class. Pattern Singleton
        /// </summary>
        private static readonly Settings settings = new Settings();

        private Settings()
        {

        }

        public static Settings GetSettings()
        {
            return settings;
        }
    }
}
