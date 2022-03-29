using System;

namespace Hotel.Booking.API.Exceptions
{
    public class RoomAvailabilityException : Exception
    {
        public RoomAvailabilityException(string message)
            : base(message)
        {
        }

        public RoomAvailabilityException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
