using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingClassLibrary
{
    public class Car : IDisposable
    {
        /// <summary>
        /// Identifier of car
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Car account balance
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// Car type from enum CarTypes
        /// </summary>
        public CarTypes CarType { get; set; }

        /// <summary>
        /// Number of parking place
        /// </summary>
        public int NumberOfParkingPlace { get; set; }

        /// <summary>
        /// UsedCounter used for realizing Id property as autoincrement
        /// </summary>
        private static List<bool> UsedCounter = new List<bool>();

        private static object Lock = new object();

        /// <summary>
        /// Instance of Car class WITHOUT starting Balance (Balance property)
        /// </summary>
        /// <param name="carType">Type of Car</param>
        /// <param name="numberOfParkingPlace">Number of parking place</param>
        public Car(CarTypes carType, int numberOfParkingPlace)
        {
            SetIdOfCar();

            CarType = carType;

            NumberOfParkingPlace = numberOfParkingPlace;

            Balance = 0D;
        }

        public Car()
        {

        }

        /// <summary>
        /// Instance of Car class WITH starting Balance (Balance property)
        /// </summary>
        /// <param name="carType">Type of Car</param>
        /// <param name="numberOfParkingPlace">Number of parking place</param>
        /// <param name="defaultBalance">Starting parking balance of car</param>
        public Car(CarTypes carType, int numberOfParkingPlace, double defaultBalance)
        {
            SetIdOfCar();

            CarType = carType;

            NumberOfParkingPlace = numberOfParkingPlace;

            if (defaultBalance < 0)
            {
                throw new CarBalanceLessZeroException("Starting balance less than zero!");
            }
            else
            {
                Balance = defaultBalance;
            }
        }

        public void Dispose()
        {
            lock (Lock)
            {
                UsedCounter[Id] = false;
            }
        }

        /// <summary>
        /// Get available index for Id property
        /// </summary>
        /// <returns>Available Id for instance of car</returns>
        private int GetAvailableIndex()
        {
            for (int i = 0; i < UsedCounter.Count; i++)
            {
                if (UsedCounter[i] == false)
                {
                    return i;
                }
            }

            // Nothing available.
            return -1;
        }

        /// <summary>
        /// Set Id of car
        /// </summary>
        private void SetIdOfCar()
        {
            lock (Lock)
            {
                int nextIndex = GetAvailableIndex();
                if (nextIndex == -1)
                {
                    nextIndex = UsedCounter.Count;
                    UsedCounter.Add(true);
                }

                Id = nextIndex;
            }
        }
    }
}
