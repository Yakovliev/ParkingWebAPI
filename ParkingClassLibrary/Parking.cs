using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParkingClassLibrary
{
    public class Parking
    {
        /// <summary>
        /// List of cars
        /// </summary>
        public List<Car> ListOfCars { get; private set; } = new List<Car>();

        /// <summary>
        /// List of transaction
        /// </summary>
        public List<Transaction> ListOfTransactions { get; private set; } = new List<Transaction>();

        /// <summary>
        /// Balance of parking
        /// </summary>
        public double Balance { get; private set; } = 0;

        /// <summary>
        /// Instance of Parking class. Pattern Singleton
        /// </summary>
        private static readonly Parking parking = new Parking();

        /// <summary>
        /// Settings of parking
        /// </summary>
        public Settings Settings { get; private set; }

        /// <summary>
        /// Free parking places list
        /// </summary>
        private static List<bool> FreeParkingPlacesList;

        private Parking()
        {
            Settings = Settings.GetSettings();

            FreeParkingPlacesList = new List<bool>();

            //All parking places are free when parking initializing
            for (int i = 0; i < Settings.ParkingSpace; i++)
            {
                FreeParkingPlacesList.Add(true);
            }
        }

        public static Parking GetParking()
        {
            return parking;
        }

        /// <summary>
        /// If method return true than there is free parking place, 
        /// if meghod return false than there are no free parking places.
        /// </summary>
        /// <returns>If method return true than there is free parking place.</returns>
        private bool IsThereFreeParkingPlace()
        {
            bool freeParkingPlace = false;

            foreach (bool item in FreeParkingPlacesList)
            {
                if (item == true)
                {
                    freeParkingPlace = true;
                    break;
                }
            }

            return freeParkingPlace;
        }

        /// <summary>
        /// Add car on parking WITHOUT starting balance.
        /// </summary>
        /// <param name="carType">Type of car</param>
        public void AddCar(CarTypes carType)
        {
            bool freeParkingPlace = IsThereFreeParkingPlace();

            int numberOfFreeParkingPlace = 0;

            if (freeParkingPlace)
            {
                for (int i = 0; i < FreeParkingPlacesList.Count; i++)
                {
                    if (FreeParkingPlacesList[i] == true)
                    {
                        numberOfFreeParkingPlace = i;
                        break;
                    }
                }

                ListOfCars.Add(new Car(carType, numberOfFreeParkingPlace));
                FreeParkingPlacesList[numberOfFreeParkingPlace] = false;
            }
            else
            {
                throw new NoFreeParkingPlacesException("No free parking places!");
            }
        }

        /// <summary>
        /// Add car on parking WITH starting balance.
        /// </summary>
        /// <param name="carType">Type of car</param>
        /// <param name="defaultBalance">Starting balance of car</param>
        public void AddCar(CarTypes carType, double defaultBalance)
        {
            bool freeParkingPlace = IsThereFreeParkingPlace();

            int numberOfFreeParkingPlace = 0;

            if (freeParkingPlace)
            {
                for (int i = 0; i < FreeParkingPlacesList.Count; i++)
                {
                    if (FreeParkingPlacesList[i] == true)
                    {
                        numberOfFreeParkingPlace = i;
                        break;
                    }
                }

                ListOfCars.Add(new Car(carType, numberOfFreeParkingPlace, defaultBalance));
                FreeParkingPlacesList[numberOfFreeParkingPlace] = false;

            }
            else
            {
                throw new NoFreeParkingPlacesException("No free parking places!");
            }
        }

        /// <summary>
        /// Delete a car by number of parking place.
        /// </summary>
        /// <param name="numberOfParkingPlace">Number of parking place where is located the car which user wants to delete.</param>
        public void DeleteCarByNumberOfParkingPlace(int numberOfParkingPlace)
        {
            //numberOfParkingPlace = 0, 1, 2, ..., Settings.ParkingSpace - 1
            if (numberOfParkingPlace > Settings.ParkingSpace - 1)
            {
                throw new NumberOfParkingPlaceDoesNotExistException("Number of parking place does not exist!");
            }

            if (FreeParkingPlacesList[numberOfParkingPlace])
            {
                throw new ParkingPlaceIsFreeException("We can`t delete a car! Parking place is already free!");
            }

            int counterOfDeleteCar = 0;

            foreach (Car item in ListOfCars)
            {
                if (item.NumberOfParkingPlace != numberOfParkingPlace)
                {
                    counterOfDeleteCar++;
                }
                else
                {
                    break;
                }
            }

            if (ListOfCars[counterOfDeleteCar].Balance < 0)
            {
                throw new CarBalanceLessZeroException("You can`t delete the car. Balance less zero!");
            }

            //Можна доробити, щоб при видаленні машини спрацьовував метод Dispose() класу Car. Тоді будуть вивільнятися id видалених
            //і вони будуть використані для створення нових машин.
            ListOfCars.RemoveAt(counterOfDeleteCar);
            FreeParkingPlacesList[numberOfParkingPlace] = true;
        }

        /// <summary>
        /// Delete a car by id.
        /// </summary>
        /// <param name="idOfCar">Id of car.</param>
        public void DeleteCarById(int idOfCar)
        {
            int counterOfId = 0;

            foreach (Car item in ListOfCars)
            {
                if (item.Id == idOfCar)
                {
                    break;
                }
                else
                {
                    counterOfId++;
                }
            }

            if (counterOfId >= ListOfCars.Count)
            {
                throw new IdOfCarDoesNotExistException("Id of car does not exist!");
            }

            if (ListOfCars[counterOfId].Balance < 0)
            {
                throw new CarBalanceLessZeroException("You can`t delete the car. Balance less zero!");
            }

            FreeParkingPlacesList[ListOfCars[counterOfId].NumberOfParkingPlace] = true;
            ListOfCars.RemoveAt(counterOfId);
        }

        /// <summary>
        /// Replenish balance of car by id.
        /// </summary>
        /// <param name="idOfCar">Id of car</param>
        /// <param name="amount">Amount of money</param>
        public void ReplenishCarBalanceById(int idOfCar, double amount)
        {
            if (amount <= 0)
            {
                throw new AmountOfMoneyLessZeroException("Amount of money less or equal zero!");
            }

            int counterOfId = 0;

            foreach (Car item in ListOfCars)
            {
                if (item.Id == idOfCar)
                {
                    break;
                }
                else
                {
                    counterOfId++;
                }
            }

            if (counterOfId >= ListOfCars.Count)
            {
                throw new IdOfCarDoesNotExistException("Id of car does not exist!");
            }

            ListOfCars[counterOfId].Balance += amount;
        }

        /// <summary>
        /// Get number of free parking places.
        /// </summary>
        /// <returns>Number of free parking places.</returns>
        public int GetFreeParkingPlaces()
        {
            int numberOfFreeParkingPlaces = 0;

            foreach (bool item in FreeParkingPlacesList)
            {
                if (item)
                {
                    numberOfFreeParkingPlaces++;
                }
            }

            return numberOfFreeParkingPlaces;
        }

        /// <summary>
        /// Get number of occupied parking places.
        /// </summary>
        /// <returns>Number of occupied parking places.</returns>
        public int GetOccupiedParkingPlaces()
        {
            int numberOfOccupiedParkingPlaces = 0;

            foreach (bool item in FreeParkingPlacesList)
            {
                if (!item)
                {
                    numberOfOccupiedParkingPlaces++;
                }
            }

            return numberOfOccupiedParkingPlaces;
        }

        /// <summary>
        /// Write off funds
        /// </summary>
        public void WriteOffFunds()
        {
            DateTime dateTimeNow = DateTime.Now;

            foreach (Car item in ListOfCars)
            {
                double writtenOffFunds;

                if (item.Balance >= 0)
                {
                    writtenOffFunds = Settings.ParkingPrices[item.CarType];
                }
                else
                {
                    writtenOffFunds = Settings.Fine * Settings.ParkingPrices[item.CarType];
                }

                item.Balance -= writtenOffFunds;
                ListOfTransactions.Add(new Transaction(dateTimeNow, item.Id, writtenOffFunds));

                Balance += writtenOffFunds;
            }
        }

        /// <summary>
        /// Get StringBuilder instance with transaction for last minute.
        /// </summary>
        /// <returns>StringBuilder instance with transaction for last minute</returns>
        public StringBuilder GetTransactionsForLastMinute()
        {
            DateTime dateTimeNow = DateTime.Now;
            DateTime dateTimeNowMinuseOneMinute = dateTimeNow.Subtract(new TimeSpan(0, 1, 0));

            StringBuilder stringBuilderOfTransaction = new StringBuilder();

            foreach (Transaction item in ListOfTransactions)
            {
                if (item.DateTimeOfTransaction > dateTimeNowMinuseOneMinute)
                {
                    stringBuilderOfTransaction.AppendLine(item.ToString());
                }
            }

            return stringBuilderOfTransaction;
        }

        /// <summary>
        /// Get earned funds for last minute.
        /// </summary>
        /// <returns>Earned funds for last minute</returns>
        public double GetEarnedFundsForLastMinute()
        {
            double earnedFundsForLastMinute = 0;

            DateTime dateTimeNow = DateTime.Now;
            DateTime dateTimeNowMinuseOneMinute = dateTimeNow.Subtract(new TimeSpan(0, 1, 0));

            foreach (Transaction item in ListOfTransactions)
            {
                if (item.DateTimeOfTransaction > dateTimeNowMinuseOneMinute)
                {
                    earnedFundsForLastMinute += item.WrittenOffFunds;
                }
            }

            return earnedFundsForLastMinute;
        }

        /// <summary>
        /// Write transactions for last minute into Transaction.log file.
        /// </summary>
        public void WriteAtTransactionLog()
        {
            double earnedFundsForLastMinute = 0;

            DateTime dateTimeNow = DateTime.Now;
            DateTime dateTimeNowMinuseOneMinute = dateTimeNow.Subtract(new TimeSpan(0, 1, 0));

            foreach (Transaction item in ListOfTransactions)
            {
                if (item.DateTimeOfTransaction > dateTimeNowMinuseOneMinute)
                {
                    earnedFundsForLastMinute += item.WrittenOffFunds;
                }
            }

            string transactionLogString = dateTimeNow.ToLongDateString() + " " + dateTimeNow.ToLongTimeString() + "   Earned funds for last minute: " +
                earnedFundsForLastMinute.ToString("#.##");

            using (StreamWriter streamWriter = new StreamWriter("Transaction.log", true))
            {
                streamWriter.WriteLine(transactionLogString);
            }
        }

        /// <summary>
        /// Read date from Transaction.log
        /// </summary>
        /// <returns>Date from Transaction.log</returns>
        public string ReadTransactionLog()
        {
            string dateFromTransactionLog = "";

            using (StreamReader streamReader = new StreamReader("Transaction.log"))
            {
                dateFromTransactionLog = streamReader.ReadToEnd();
            }

            return dateFromTransactionLog;
        }

        /// <summary>
        /// Get StringBuilder of all cars.
        /// </summary>
        /// <returns>StringBuilder instance of all cars</returns>
        public StringBuilder GetAllCars()
        {
            StringBuilder stringBuilderOfAllCars = new StringBuilder();

            foreach (Car item in ListOfCars)
            {
                stringBuilderOfAllCars.AppendLine("Id of the car: " + item.Id.ToString() + "  Type of the car: " +
                    item.CarType.ToString() + "  Balance of the car: " + item.Balance);
            }

            return stringBuilderOfAllCars;
        }

        // <summary>
        /// Get details on the car.
        /// </summary>
        /// <returns>Details on the car</returns>
        public string GetCarDetails(int idOfCar)
        {
            string details = "";

            int counterOfId = 0;

            foreach (Car item in ListOfCars)
            {
                if (item.Id == idOfCar)
                {
                    break;
                }
                else
                {
                    counterOfId++;
                }
            }

            if (counterOfId >= ListOfCars.Count)
            {
                throw new IdOfCarDoesNotExistException("Id of car does not exist!");
            }

            details = "Id of the car: " + ListOfCars[counterOfId].Id.ToString() + "  Type of the car: " +
                    ListOfCars[counterOfId].CarType.ToString() + "  Balance of the car: " + ListOfCars[counterOfId].Balance;

            return details;
        }


        /// <summary>
        /// Get StringBuilder instance with transaction of the car for last minute.
        /// </summary>
        public StringBuilder GetTransactionsOfCarForLastMinute(int idOfCar)
        {
            DateTime dateTimeNow = DateTime.Now;
            DateTime dateTimeNowMinuseOneMinute = dateTimeNow.Subtract(new TimeSpan(0, 1, 0));

            int counterOfId = 0;

            foreach (Car item in ListOfCars)
            {
                if (item.Id == idOfCar)
                {
                    break;
                }
                else
                {
                    counterOfId++;
                }
            }

            if (counterOfId >= ListOfCars.Count)
            {
                throw new IdOfCarDoesNotExistException("Id of car does not exist!");
            }

            StringBuilder stringBuilderOfTransaction = new StringBuilder();

            foreach (Transaction item in ListOfTransactions)
            {
                if (item.DateTimeOfTransaction > dateTimeNowMinuseOneMinute && item.IdOfCar == idOfCar)
                {
                    stringBuilderOfTransaction.AppendLine("Id of car: " + item.IdOfCar + "  " + 
                        item.DateTimeOfTransaction.ToLongDateString() + item.DateTimeOfTransaction.ToLongTimeString() + "  WrittenOffFunds: " +
                        item.WrittenOffFunds.ToString());
                }
            }

            return stringBuilderOfTransaction;
        }
    }
}
