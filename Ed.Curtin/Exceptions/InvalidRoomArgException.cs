using System;

namespace Hotel.Booking.API.Exceptions
{
    public class InvalidRoomArgException : Exception
    {
        public InvalidRoomArgException(string message)
            : base(message)
        {
        }

        public InvalidRoomArgException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
