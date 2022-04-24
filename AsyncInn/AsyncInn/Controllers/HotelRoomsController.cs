using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.Interfaces;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _HotelRoom;

        public HotelRoomsController(IHotelRoom hotelRoom)
        {
            _HotelRoom = hotelRoom;
        }

        // GET: api/HotelRooms/1/1
        [HttpGet("{hotelId}/{roomNumber}")]
        public async Task<IActionResult> GetRoomDetails(int hotelId, int roomNumber)
        {
            var room = await _HotelRoom.RoomDetails(hotelId, roomNumber);
            return Ok(room);
        }

        // GET: api/HotelRooms/1
        [HttpGet("{hotelId}")]
        public async Task<ActionResult<Hotel>> GetHotelRooms(int hotelId)
        {
            var hotelRooms = await _HotelRoom.GetHotelRooms(hotelId);
            return Ok(hotelRooms);
        }

        [HttpPut("{hotelId}/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, Room room)
        {
            var newRoom = await _HotelRoom.UpdateRoomDetails(hotelId, roomNumber, room);
            return Ok(newRoom);
        }

        // POST: api/HotelRooms/3/1/1
        [HttpPost("{hotelId}/{roomId}/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(int hotelId, int roomId, int roomNumber)
        {
            var hotelRoom = await _HotelRoom.AddRoomToHotel(hotelId, roomId, roomNumber);
            return Ok(hotelRoom);
        }

        // DELETE: api/HotelRooms/1/1
        [HttpDelete("{hotelId}/{roomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            await _HotelRoom.DeleteRoomFromHotel(hotelId, roomNumber);
            return NoContent();
        }
    }
}
