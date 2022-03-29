using Hotel.Booking.API.Exceptions;
using System;
using System.Linq;
using Xunit;

namespace Hotel.Booking.API.DAL.UnitTest
{
    public class BookingManagerUnitTests: IDisposable
    {
        private BookingManager _bookingManager;
        public BookingManagerUnitTests()
        {
            _bookingManager = BookingManager.Instance;
        }


        [Fact]
        public void TestIsRoomAvailableProvidedInvalidRoomNumberThrowsInvalidRoomArgException()
        {
            Assert.Throws<InvalidRoomArgException>(() => _bookingManager.IsRoomAvailable(1, DateTime.Now));
        }

        [Fact]
        public void TestIsRoomAvailableProvidedCorrectRoomNumberAndDateReturnsBoolean()
        {
            Assert.True(_bookingManager.IsRoomAvailable(101, DateTime.Now).GetType() == typeof(bool));
        }

        [Fact]
        public void TestIsRoomAvailableWithSameRoomNumberAndDatePreAndPostBooking()
        {
            var inputDate = DateTime.Now;

            Assert.True(_bookingManager.IsRoomAvailable(101, inputDate));
            _bookingManager.AddBooking("Ed", 101, inputDate);
            Assert.False(_bookingManager.IsRoomAvailable(101, inputDate));
        }

        [Fact]
        public void TestAddBookingThrowsInvalidRoomExceptionWhenProvidedIncorrectRoomNumber()
        {
            Assert.Throws<InvalidRoomArgException>(() => _bookingManager.AddBooking("DummyPerson", 1, DateTime.Now));
        }

        [Fact]
        public void TestAddBookingThrowsRoomAvailabilityExceptionIfTryToBookTwiceOnSameDate()
        {
            var todaysDate = DateTime.Now.Date;
            var roomNumber = 101;
            _bookingManager.AddBooking("DummyPerson", roomNumber, todaysDate);
            Assert.Throws<RoomAvailabilityException>(() => _bookingManager.AddBooking("DummyPerson1", roomNumber, todaysDate));
        }

        [Fact]
        public void TestGetAvailableRoomsReturnsCollectionMatchingHotelRoomsBeforeAnyBookings()
        {
            DateTime today = DateTime.Now;

            var availableRooms = _bookingManager.GetAvailableRooms(today);
            Assert.Equal(4, availableRooms.ToList<int>().Count);
        }

        [Fact]
        public void TestGetAvailableRoomsReturnsCorrectSizeCollectionPriorAndAfterBooking()
        {
            DateTime today = DateTime.Now;

            var availableRooms = _bookingManager.GetAvailableRooms(today);
            Assert.Equal(4, availableRooms.ToList<int>().Count);
            _bookingManager.AddBooking("Tom", 101, today);
            availableRooms = _bookingManager.GetAvailableRooms(today);
            Assert.Equal(3, availableRooms.ToList<int>().Count);

        }

        public void Dispose()
        {
          _bookingManager.Dispose();
        }
    }
}
