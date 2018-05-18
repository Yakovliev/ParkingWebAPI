using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ParkingClassLibrary
{
    public class Menu
    {
        /// <summary>
        /// Instance of Menu class. Pattern Singleton
        /// </summary>
        private static readonly Menu menu = new Menu();

        /// <summary>
        /// Parking
        /// </summary>
        public Parking Parking { get; private set; }

        private Menu()
        {
            Parking = Parking.GetParking();

            string path = "Transaction.log";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            WriteOffFunds();

            WriteAtTransactionLog();
        }

        public static Menu GetMenu()
        {
            return menu;
        }

        /// <summary>
        /// Add car on parking WITHOUT starting balance.
        /// </summary>
        /// <param name="carType">Type of car</param>
        public void AddCar(CarTypes carType)
        {
            try
            {
                Parking.AddCar(carType);
                //Console.WriteLine("Done!");
            }
            catch (NoFreeParkingPlacesException)
            {
                //Console.WriteLine("No free parking places!");
            }
        }

        /// <summary>
        /// Add car on parking WITH starting balance.
        /// </summary>
        /// <param name="carType">Type of car</param>
        /// <param name="defaultBalance">Starting balance of car</param>
        /// <param name="carType">Type of car</param>
        public void AddCar(CarTypes carType, double defaultBalance)
        {
            try
            {
                Parking.AddCar(carType, defaultBalance);
                //Console.WriteLine("Done!");
            }
            catch (NoFreeParkingPlacesException)
            {
                //Console.WriteLine("No free parking places!");
            }
            catch (CarBalanceLessZeroException)
            {
                //Console.WriteLine("Starting balance of car cannot be less zero!");
            }
        }

        /// <summary>
        /// Delete a car by number of parking place.
        /// </summary>
        /// <param name="numberOfParkingPlace">Number of parking place where is located the car which user wants to delete.</param>
        public void DeleteCarByNumberOfParkingPlace(int numberOfParkingPlace)
        {
            try
            {
                Parking.DeleteCarByNumberOfParkingPlace(numberOfParkingPlace);
                //Console.WriteLine("Done!");
            }
            catch (NumberOfParkingPlaceDoesNotExistException)
            {
                //Console.WriteLine("Such number of parking place does not exist! Try another number!");
            }
            catch (ParkingPlaceIsFreeException)
            {
                //Console.WriteLine("You can`t delete car, because parking place are already free!");
            }
            catch (CarBalanceLessZeroException)
            {
                //Console.WriteLine("Car balance is less zero! Please replenish balance of car car");
            }
        }

        /// <summary>
        /// Delete a car by id.
        /// </summary>
        /// <param name="idOfCar">Id of car.</param>
        public string DeleteCarById(int idOfCar)
        {
            try
            {
                Parking.DeleteCarById(idOfCar);
                return "Car Deleted";
            }
            catch (IdOfCarDoesNotExistException)
            {
                return "Car IdOfCarDoesNotExistException";
            }
            catch (CarBalanceLessZeroException)
            {
                return "Car CarBalanceLessZeroException";
            }
        }

        /// <summary>
        /// Replenish balance of car by id.
        /// </summary>
        /// <param name="idOfCar">Id of car</param>
        /// <param name="amount">Amount of money</param>
        public string ReplenishCarBalanceById(int idOfCar, double amount)
        {
            try
            {
                Parking.ReplenishCarBalanceById(idOfCar, amount);
                return "Balance of the car with id " + idOfCar.ToString() +
                    "increased by " + amount.ToString();
            }
            catch (IdOfCarDoesNotExistException)
            {
                return "Such id of car does not exist!";
            }
            catch (AmountOfMoneyLessZeroException)
            {
                return "Amount of money less or equal zero!";
            }
        }

        /// <summary>
        /// Write number of free parking places.
        /// </summary>
        public int GetFreeParkingPlaces()
        {
            return Parking.GetFreeParkingPlaces();
            //Console.WriteLine("Number of free parking places = {0}", numberOfFreeParkingPlaces);
        }

        /// <summary>
        /// Write number of occupied parking places.
        /// </summary>
        public int GetOccupiedParkingPlaces()
        {
            return Parking.GetOccupiedParkingPlaces();
            //Console.WriteLine("Number of occupied parking places = {0}", numberOfOccupiedParkingPlaces);
        }

        /// <summary>
        /// Write off funds
        /// </summary>
        public void WriteOffFunds()
        {
            var thread = new Thread(WriteOffFundsThread);
            thread.Start();
        }

        private static void WriteOffFundsThread()
        {
            Parking parking = Parking.GetParking();

            while (true)
            {
                Thread.Sleep(parking.Settings.Timeout * 1000);
                parking.WriteOffFunds();
                //Console.WriteLine("Written-off"); //Test
            }
        }

        /// <summary>
        /// Get StringBuilder instance with transaction for last minute.
        /// </summary>
        public string GetTransactionsForLastMinute()
        {
            return Parking.GetTransactionsForLastMinute().ToString();

            //Console.WriteLine(stringBuilder.ToString());
        }

        /// <summary>
        /// Get earned funds for last minute.
        /// </summary>
        public void GetEarnedFundsForLastMinute()
        {
            double earnedFundsForLastMinute = 0;

            earnedFundsForLastMinute = Parking.GetEarnedFundsForLastMinute();

            //Console.WriteLine("Earned funds for last minute: {0}", earnedFundsForLastMinute);
        }

        /// <summary>
        /// Write transactions for last minute into Transaction.log file every minute.
        /// </summary>
        public void WriteAtTransactionLog()
        {
            var thread = new Thread(WriteAtTransactionLogThread);
            thread.Start();
        }

        private static void WriteAtTransactionLogThread()
        {
            Parking parking = Parking.GetParking();

            while (true)
            {
                Thread.Sleep(60 * 1000);
                parking.WriteAtTransactionLog();
            }
        }

        /// <summary>
        /// Read date from Transaction.log
        /// </summary>
        public string ReadTransactionLog()
        {
            return Parking.ReadTransactionLog();

            //Console.WriteLine(dateFromTransactionLog);
        }

        public string GetAllCars()
        {
            return Parking.GetAllCars().ToString();
        }

        public string GetCarDetails(int idOfCar)
        {
            try
            {
                return Parking.GetCarDetails(idOfCar);
            }
            catch (IdOfCarDoesNotExistException)
            {
                return "Id of car does not exist!";
            }
        }

        public double GetParkingBalance()
        {
            return Parking.Balance;
        }

        /// <summary>
        /// Get transactions of the car for last minute.
        /// </summary>
        public string GetTransactionsOfCarForLastMinute(int idOfCar)
        {
            try
            {
                return Parking.GetTransactionsOfCarForLastMinute(idOfCar).ToString();
            }
            catch (IdOfCarDoesNotExistException)
            {
                return "Id of car does not exist!";
            }
        }

    }
}
