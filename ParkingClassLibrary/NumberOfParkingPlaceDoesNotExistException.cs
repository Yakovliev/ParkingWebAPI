using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingClassLibrary
{
    public class NumberOfParkingPlaceDoesNotExistException : Exception
    {
        public NumberOfParkingPlaceDoesNotExistException(string message)
            : base(message)
        {

        }
    }
}
