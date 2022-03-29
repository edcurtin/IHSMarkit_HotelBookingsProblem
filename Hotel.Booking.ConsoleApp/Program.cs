using Hotel.Booking.API.DAL;
using Hotel.Booking.API.Exceptions;
using System;
using AwesomeHotel = Hotel.Booking.API.Models.Hotel;

namespace Hotel.Booking.ConsoleApp
{
    class Program
    {
        public static IBookingManager _bookingManager;
        static void Main(string[] args)
        {
            try
            {
                _bookingManager = BookingManager.Instance;
                var today = new DateTime(2012, 3, 28);
                Console.WriteLine(_bookingManager.IsRoomAvailable(101, today)); // outputs true 
                _bookingManager.AddBooking("Patel", 101, today);
                Console.WriteLine(_bookingManager.IsRoomAvailable(101, today)); // outputs false 
                //_bookingManager.AddBooking("Li", 101, today); // throws an exception

                //var allAvailableRoomsForDate = _bookingManager.GetAvailableRooms(today);
                //if (allAvailableRoomsForDate != null)
                //{
                //    foreach (int roomNumber in allAvailableRoomsForDate)
                //    {

                //        var availabilityMessage = String.Format("Room Free for Date: {0}, {1}",
                //       roomNumber, today.Date);

                //        Console.WriteLine(availabilityMessage);
                //    }
                //}

            }
            catch (RoomAvailabilityException roomAvailability)
            {
                Console.WriteLine("Exception: " + roomAvailability.Message);
            }
            catch (InvalidRoomArgException invalidRoomArgProvided)
            {
                Console.WriteLine("Exception: " + invalidRoomArgProvided.Message);
            }


            Console.ReadLine();


        }
    }
}
