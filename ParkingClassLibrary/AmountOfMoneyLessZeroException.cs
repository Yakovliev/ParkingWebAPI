using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingClassLibrary
{
    public class AmountOfMoneyLessZeroException : Exception
    {
        public AmountOfMoneyLessZeroException(string message)
            : base(message)
        {

        }
    }
}
