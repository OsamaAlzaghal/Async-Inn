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

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            return await _context.HotelRooms.Where(x => x.HotelID == hotelId).Include(x => x.Room).ToListAsync();
                                 
        }

        public async Task<HotelRoom> RoomDetails(int hotelId, int roomNumber)
        {

            var hotelRoom = await _context.HotelRooms
                                          .Where(x => x.HotelID == hotelId & x.RoomNumber == roomNumber)
                                          .Include(x => x.Room)
                                          .ThenInclude(x => x.RoomAmenities)
                                          .Include(x => x.Hotel)
                                          .FirstOrDefaultAsync();

            return hotelRoom;
        }

        // Fixed.
        public async Task<HotelRoom> UpdateRoomDetails(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            var oldhotelRoom = await _context.HotelRooms
                                          .Where(x => x.HotelID == hotelId & x.RoomNumber == roomNumber)
                                          .FirstAsync();

            oldhotelRoom.PetFriendly = hotelRoom.PetFriendly;
            oldhotelRoom.Rate = hotelRoom.Rate;

            _context.Entry(oldhotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldhotelRoom;
        }
    }
}
