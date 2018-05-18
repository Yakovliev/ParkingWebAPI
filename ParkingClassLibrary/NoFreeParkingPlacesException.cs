using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingClassLibrary
{
    public class NoFreeParkingPlacesException : Exception
    {
        public NoFreeParkingPlacesException(string message)
            : base(message)
        {

        }
    }
}
