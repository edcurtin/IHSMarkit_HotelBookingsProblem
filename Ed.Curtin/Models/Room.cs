using System.Collections.Concurrent;

namespace Hotel.Booking.API.Models
{
    public class Room
    {
        public Room(int roomNumber)
        {
            RoomNumber = roomNumber;
            RoomBookings = new BlockingCollection<RoomBooking>();
        }

        public BlockingCollection<RoomBooking> RoomBookings { get; set; }
        public int RoomNumber { get; private set; }

    }
}
