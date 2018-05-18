using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingClassLibrary
{
    public class ParkingPlaceIsFreeException : Exception
    {
        public ParkingPlaceIsFreeException(string message)
            : base(message)
        {

        }
    }
}
