using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingClassLibrary
{
    public class IdOfCarDoesNotExistException : Exception
    {
        public IdOfCarDoesNotExistException(string message)
            : base(message)
        {

        }
    }
}
