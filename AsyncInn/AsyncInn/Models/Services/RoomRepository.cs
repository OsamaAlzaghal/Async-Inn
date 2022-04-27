using AsyncInn.Data;
using AsyncInn.Models.DTOs;
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

        public async Task<Room> Create(RoomDTO room)
        {
            Room newRoom = new Room
            {
                Name = room.Name,
                Layout = room.Layout,
            };
            _context.Entry(newRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return newRoom;
        }
        public async Task<List<RoomDTO>> GetRooms()
        {
            //var rooms = await _context.Rooms.ToListAsync();
            //return rooms;

            return await _context.Rooms.Select(x => new RoomDTO
            {
                ID = x.ID,
                Name = x.Name,
                Layout = x.Layout,
                Amenities = x.RoomAmenities.Select(x => new AmenityDTO
                {
                    ID = x.AmenityID,
                    Name = x.Amenity.Name
                }).ToList()
            }).ToListAsync();
        }
        public async Task<RoomDTO> GetRoom(int id)
        {
            return await _context.Rooms.Select(x => new RoomDTO
            {
                ID = x.ID,
                Name = x.Name,
                Layout = x.Layout,
                Amenities = x.RoomAmenities.Select(x => new AmenityDTO
                {
                    ID = x.AmenityID,
                    Name = x.Amenity.Name
                }).ToList()
            }).FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task Delete(int id)
        {
            Room room = await _context.Rooms.FirstOrDefaultAsync(x => x.ID == id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        public async Task<Room> UpdateRoom(int id, RoomDTO room)
        {
            Room newRoom = new Room
            {
                ID = id,
                Name = room.Name,
                Layout = room.Layout
            };
            _context.Entry(newRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return newRoom;
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

            var roomAmenity = await _context.RoomAmenities.Where(x => x.AmenityID == amenityId && x.RoomID == roomId)
                                                          .FirstOrDefaultAsync();
            if(roomAmenity != null)
            {
                _context.Entry(roomAmenity).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
