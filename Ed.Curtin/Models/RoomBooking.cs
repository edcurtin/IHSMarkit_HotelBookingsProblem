
using System;

namespace Hotel.Booking.API.Models
{
    public class RoomBooking
    {
        public RoomBooking(Guest guest, Room room, DateTime dateOfBooking)
        {
            OwnerOfBooking = guest;
            DateOfBooking = dateOfBooking;
            Room = room;
        }

        public Room Room { get; private set; }
        public Guest OwnerOfBooking { get; private set; }
        public DateTime? DateOfBooking { get; private set; }
    }
}
