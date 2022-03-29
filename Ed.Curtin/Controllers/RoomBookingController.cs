using Hotel.Booking.API.DAL;
using Hotel.Booking.API.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Hotel.Booking.API.Controllers
{
    [ApiController]
    [Route("api/RoomBooking")]
    public class RoomBookingController : Controller
    {
        private readonly ILogger<RoomBookingController> _logger;
        private readonly IBookingManager _bookingManager;

        public RoomBookingController(ILogger<RoomBookingController> logger, IBookingManager bookingManager)
        {
            _logger = logger;
            _bookingManager = bookingManager;
        }

        [HttpGet("CheckForAvailableRooms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAvailableRoomsForDate(DateTime dateToCheck)
        {
            List<int> availableRoomsList = new List<int>();

            try
            {
                var availableRooms = _bookingManager.GetAvailableRooms(dateToCheck);
                return Ok(availableRooms);
            }
            catch (InvalidRoomArgException invalidRoomException)
            {
                _logger.LogError(invalidRoomException, null);
                return BadRequest();
            }
        }

        // If Driving from Swagger date is in the following format: 2019-07-01T04:00:00.000Z
        [HttpGet("CheckAvailability")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetIsRoomAvailableForDate(int roomNumber, DateTime dateToCheck)
        {
            try
            {
                var isRoomAvailable = _bookingManager.IsRoomAvailable(roomNumber, dateToCheck);
                return Ok(isRoomAvailable);
            }
            catch (RoomAvailabilityException availabiltyException)
            {
                _logger.LogError(availabiltyException, null);
                return BadRequest();
            }
            catch(InvalidRoomArgException invalidRoomException)
            {
                _logger.LogError(invalidRoomException, null);
                return BadRequest();
            }
        }

        [HttpPost("MakeBooking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BookRoom(string guestName, int roomNumber, DateTime dateToBook)
        {
            try
            {
                 _bookingManager.AddBooking(guestName, roomNumber, dateToBook);
                return Ok();
            }
            catch (RoomAvailabilityException availabiltyException)
            {
                _logger.LogError(availabiltyException, null);
                return BadRequest();
            }
            catch (InvalidRoomArgException invalidRoomException)
            {
                _logger.LogError(invalidRoomException, null);
                return BadRequest();
            }
        } 



    }
}
