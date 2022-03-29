using Hotel.Booking.API.Exceptions;
using Hotel.Booking.API.Models;
using System;
using System.Collections.Generic;
using AwesomeHotel = Hotel.Booking.API.Models.Hotel;

namespace Hotel.Booking.API.DAL
{
    public class BookingManager : IBookingManager, IDisposable
    {
        private AwesomeHotel _hotel;
        private object _lock = new object(); // lock in order to allow thread safe access

        public static BookingManager Instance = new BookingManager();

        private BookingManager()
        {
            _hotel = new AwesomeHotel();
        }

        /// <summary>
        /// AddBooking
        /// </summary>
        /// <param name="guestName"></param>
        /// <param name="room"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public void AddBooking(string guestName, int room, DateTime date)
        {
            // check hotel room is correct
            if (CheckHotelRoomIsValid(room) == false)
                throw new InvalidRoomArgException("Hotel Room Provided is Invalid");

            if (IsRoomAvailable(room, date))
            {
                lock (_lock)
                {
                    foreach(Room r in _hotel.HotelRooms)
                    {
                        Guest guest = new Guest(guestName);
                        Room guestRoom = new Room(room);

                        if(r.RoomBookings.TryAdd(new RoomBooking(guest, guestRoom, date.Date)))
                        {
                            return;
                        }
                    }
                }
            }
            else
            {
                throw new RoomAvailabilityException("Room Already Booked for the Given Date");
            }
            //bool roomBookedSuccessfully = false;
            //lock (_lock)
            //{
            //    foreach (Room r in _hotel.HotelRooms)
            //    {
            //        if (r.RoomNumber == room)
            //        {
            //            foreach (RoomBooking rb in r.RoomBookings)
            //            {
            //                if (rb.DateOfBooking.Equals(date))
            //                {
            //                    freeRoomsList.Add(r.RoomNumber);
            //                }
            //            }


            //            if (!r.BookingDate.Equals(date))
            //            {
            //                if (_hotel.HotelRooms.TryTake(out Room roomToUpdate))
            //                {
            //                    Guest guest = new Guest(guestName);
            //                    roomToUpdate.BookedBy = guest;
            //                    roomToUpdate.BookingDate = date;

            //                    if (_hotel.HotelRooms.TryAdd(roomToUpdate))
            //                    {
            //                        roomBookedSuccessfully = true;
            //                    }
            //                }
            //            }
            //        }

            //        if (roomBookedSuccessfully == false)
            //        {
            //            throw new RoomAvailabilityException("Room Already Booked for the Given Date");
            //        }
            //    }
            //}

        }

        /// <summary>
        /// AddBooking - Add a booking for the given guest in the given room on the given
        /// date. If the room is not available, throw a suitable Exception.
        /// </summary>
        /// <param name="guest">guest name - treated as unique</param>
        /// <param name="room">room number</param>
        /// <param name="date">date to check</param>
        public IEnumerable<int> GetAvailableRooms(DateTime date)
        {
            List<int> freeRoomsList = new List<int>();
            lock (_lock)
            {

                foreach (Room r in _hotel.HotelRooms)
                {
                    if(IsRoomAvailable(r.RoomNumber, date.Date))
                    {
                        freeRoomsList.Add(r.RoomNumber);
                    }
                }
            }

            return freeRoomsList;
        }

        /// <summary>
        /// GetAvailableRooms - Return a list of all the available room numbers for the given date
        /// </summary>
        /// <param name="date">date to check</param>
        /// <returns>IEnumerable collection of rooms that are available for a given date</returns>
        public bool IsRoomAvailable(int room, DateTime date)
        {
            // check hotel room is correct
            if (CheckHotelRoomIsValid(room) == false)
                throw new InvalidRoomArgException("Hotel Room Provided is Invalid");

            bool isRoomAvailableToBook = false;

            lock (_lock)
            {
                foreach (Room r in _hotel.HotelRooms)
                {
                    if(r.RoomNumber == room)
                    {
                        if(r.RoomBookings.Count == 0)
                        {
                            isRoomAvailableToBook = true;
                            return isRoomAvailableToBook;
                        }
                        else
                        {
                            bool isRoomAvailable = true;
                            foreach (RoomBooking rb in r.RoomBookings)
                            {
                                if (rb.DateOfBooking.HasValue)
                                {
                                    if (!rb.DateOfBooking.Value.Date.Equals(date.Date))
                                    {
                                        isRoomAvailable = false;
                                        return isRoomAvailable;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return isRoomAvailableToBook;
        }


        /// <summary>
        /// CheckHotelRoomIsValid - we dont lock as its a pure read
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <returns></returns>
        private bool CheckHotelRoomIsValid(int roomNumber)
        {
            bool isRoomValid = false;

            foreach (Room r in _hotel.HotelRooms)
            {
                if (r.RoomNumber.Equals(roomNumber))
                {
                    isRoomValid = true;
                    return isRoomValid;
                }
            }

            return isRoomValid;
        }

        public void Dispose()
        {
            _hotel = new AwesomeHotel();
        }
    }
}
