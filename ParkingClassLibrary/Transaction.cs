using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingClassLibrary
{
    public class Transaction
    {
        /// <summary>
        /// Date and time of transaction
        /// </summary>
        public DateTime DateTimeOfTransaction { get; private set; }

        /// <summary>
        /// Id of car
        /// </summary>
        public int IdOfCar { get; private set; }

        /// <summary>
        /// Written-off funds. 
        /// If WrittenOffFunds less zero, than funds are written-off
        /// If WrittenOffFunds more zero, than funds are added
        /// </summary>
        public double WrittenOffFunds { get; private set; }

        /// <summary>
        /// Constructor of Transaction class.
        /// </summary>
        /// <param name="dateTimeOfTransaction">Date and time of transaction.</param>
        /// <param name="idOfCar">Id of tbe car.</param>
        /// <param name="writtenOffFunds">Written-off funds.</param>
        public Transaction(DateTime dateTimeOfTransaction, int idOfCar, double writtenOffFunds)
        {
            DateTimeOfTransaction = dateTimeOfTransaction;

            IdOfCar = idOfCar;

            WrittenOffFunds = writtenOffFunds;
        }

        public override string ToString()
        {
            string transactionString;

            transactionString = DateTimeOfTransaction.ToLongDateString() + " " + DateTimeOfTransaction.ToLongTimeString() + "   " + "Id of car: " + IdOfCar.ToString() +
                "   Written-off funds: " + WrittenOffFunds.ToString("#.##");

            return transactionString;
        }

    }
}
