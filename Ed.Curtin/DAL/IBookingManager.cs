using System;
using System.Collections.Generic;

namespace Hotel.Booking.API.DAL
{
    public interface IBookingManager
    {
        /// <summary>
        /// IsRoomAvailable - Return true if there is no booking for the given room on the date,
        /// otherwise false
        /// </summary>
        /// <param name="room">room number</param>
        /// <param name="date">date to check</param>
        /// <returns>true if room is available for the given date otherwise returns false</returns>
        bool IsRoomAvailable(int room, DateTime date);

        /// <summary>
        /// AddBooking - Add a booking for the given guest in the given room on the given
        /// date. If the room is not available, throw a suitable Exception.
        /// </summary>
        /// <param name="guest">guest name - treated as unique</param>
        /// <param name="room">room number</param>
        /// <param name="date">date to check</param>
        void AddBooking(string guest, int room, DateTime date);

        /// <summary>
        /// GetAvailableRooms - Return a list of all the available room numbers for the given date
        /// </summary>
        /// <param name="date">date to check</param>
        /// <returns>IEnumerable collection of rooms that are available for a given date</returns>
        IEnumerable<int> GetAvailableRooms(DateTime date);
    }
}
