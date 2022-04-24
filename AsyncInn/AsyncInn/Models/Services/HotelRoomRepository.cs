using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private readonly AsyncInnDbContext _context;

        public HotelRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<HotelRoom> AddRoomToHotel(int hotelId, int roomId, int roomNumber)
        {
            var hotelRoom = new HotelRoom
            {
                HotelID = hotelId,
                RoomID = roomId,
                RoomNumber = roomNumber
            };

            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task DeleteRoomFromHotel(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms
                                          .Where(x => x.HotelID == hotelId & x.RoomNumber == roomNumber)
                                          .FirstOrDefaultAsync();

            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Hotel> GetHotelRooms(int hotelId)
        {
            return await _context.Hotels
                                  .Include(x => x.HotelRooms)
                                  .ThenInclude(x => x.Room)
                                  .FirstOrDefaultAsync(x => x.ID == hotelId);
        }

        public async Task<Room> RoomDetails(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms
                                          .Where(x => x.HotelID == hotelId & x.RoomNumber == roomNumber)
                                          .FirstAsync();

            var room = await _context.Rooms
                                     .Where(x => x.ID == hotelRoom.RoomID)
                                     .FirstAsync();
            return room;
        }

        // Needs to be fixed.
        public async Task<Room> UpdateRoomDetails(int hotelId, int roomNumber, Room room)
        {
            var hotelRoom = await _context.HotelRooms
                                          .Where(x => x.HotelID == hotelId & x.RoomNumber == roomNumber)
                                          .FirstAsync();

            hotelRoom.Room = room;
            _context.Entry(hotelRoom.Room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return room;
        }
    }
}
