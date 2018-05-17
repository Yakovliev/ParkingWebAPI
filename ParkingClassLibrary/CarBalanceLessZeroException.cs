using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingClassLibrary
{
    public class CarBalanceLessZeroException : Exception
    {
        public CarBalanceLessZeroException(string message)
            : base(message)
        {

        }
    }
}
