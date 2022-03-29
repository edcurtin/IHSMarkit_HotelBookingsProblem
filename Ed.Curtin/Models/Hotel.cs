using System.Collections.Concurrent;

namespace Hotel.Booking.API.Models
{
    public class Hotel : IHotel
    {

        public Hotel()
        {
            HotelRooms = new BlockingCollection<Room>();
            SetupHotelRooms();
        }

        /// <summary>
        /// HotelRooms 
        /// </summary>
        public BlockingCollection<Room> HotelRooms { get; private set; }

        public void SetupHotelRooms()
        {
            HotelRooms.Add(new Room(101));
            HotelRooms.Add(new Room(102));
            HotelRooms.Add(new Room(201));
            HotelRooms.Add(new Room(203));
        }

    }
}
