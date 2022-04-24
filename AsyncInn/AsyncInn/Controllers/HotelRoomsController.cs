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
        [HttpGet("{hotelId}/{roomId}")]
        public async Task<IActionResult> GetRoomDetails(int hotelId, int roomId)
        {
            var room = await _HotelRoom.RoomDetails(hotelId, roomId);
            return Ok(room);
        }

        // GET: api/HotelRooms/1
        [HttpGet("{hotelId}")]
        public async Task<ActionResult<Hotel>> GetHotelRooms(int hotelId)
        {
            var hotelRooms = await _HotelRoom.GetHotelRooms(hotelId);
            return Ok(hotelRooms);
        }

        [HttpPut("{hotelId}/{roomId}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomId, Room room)
        {
            var newRoom = await _HotelRoom.UpdateRoomDetails(hotelId, roomId, room);
            return Ok(newRoom);
        }

        // POST: api/HotelRooms/3/1
        [HttpPost("{hotelId}/{roomId}")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(int hotelId, int roomId)
        {
            var hotelRoom = await _HotelRoom.AddRoomToHotel(hotelId, roomId);
            return Ok(hotelRoom);
        }

        // DELETE: api/HotelRooms/1
        [HttpDelete("{hotelId}/{roomId}")]
        public async Task<IActionResult> DeleteHotelRoom(int hotelId, int roomId)
        {
            await _HotelRoom.DeleteRoomFromHotel(hotelId, roomId);
            return NoContent();
        }
    }
}
