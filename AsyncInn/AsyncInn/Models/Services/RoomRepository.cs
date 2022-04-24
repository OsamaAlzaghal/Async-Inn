using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class RoomRepository : IRoom
    {
        private readonly AsyncInnDbContext _context;

        public RoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Room> Create(Room room)
        {
            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return room;
        }
        public async Task<List<Room>> GetRooms()
        {
            //var rooms = await _context.Rooms.ToListAsync();
            //return rooms;

            var rooms = await _context.Rooms
                                      .Include(x => x.RoomAmenities)
                                      .ThenInclude(c => c.Amenity)
                                      .ToListAsync();
            return rooms;
        }
        public async Task<Room> GetRoom(int id)
        {
            // The system knows we have a primary key and will use it
            //Room room = await _context.Rooms.FindAsync(id);
            //return room;

            var room = await _context.Rooms.Where(x => x.ID == id)
                                            .Include(x => x.RoomAmenities)
                                            .ThenInclude(c => c.Amenity)
                                            .FirstOrDefaultAsync();
            return room;
        }

        public async Task Delete(int id)
        {
            Room room = await GetRoom(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        public async Task<Room> UpdateRoom(int id, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenity roomAmenity = new RoomAmenity { AmenityID = amenityId, RoomID = roomId };
            _context.Entry(roomAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            //Room room = await GetRoom(roomId);
            //room.RoomAmenities = null;
            //_context.Entry(room).State = EntityState.Modified;

            var roomAmenity = await _context.RoomAmenities.Where(x => x.AmenityID == amenityId & x.RoomID == roomId)
                                                          .FirstOrDefaultAsync();
            if(roomAmenity != null)
            {
                _context.Entry(roomAmenity).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
