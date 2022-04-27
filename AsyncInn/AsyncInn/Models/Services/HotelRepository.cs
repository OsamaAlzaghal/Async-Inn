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
    public class HotelRepository : IHotel
    {
        private readonly AsyncInnDbContext _context;

        public HotelRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
        }
        public async Task<List<HotelDTO>> GetHotels()
        {
            return await _context.Hotels.Select(x => new HotelDTO
            {
                ID = x.ID,
                Name = x.Name,
                StreetAddress = x.StreetAddress,
                City = x.City,
                State = x.State,
                Phone = x.Phone,
                Rooms = x.HotelRooms.Select(x => new HotelRoomDTO
                {
                    HotelID = x.HotelID,
                    RoomNumber = x.RoomNumber,
                    Rate = x.Rate,
                    PetFriendly = x.PetFriendly,
                    RoomID = x.RoomID,
                    Room = new RoomDTO
                    {
                        ID = x.Room.ID,
                        Name = x.Room.Name,
                        Layout = x.Room.Layout,
                        Amenities = x.Room.RoomAmenities.Select(x => new AmenityDTO
                        {
                            ID = x.AmenityID,
                            Name = x.Amenity.Name
                        }).ToList()

                    }
                }).ToList()
            }).ToListAsync();
        }
        public async Task<HotelDTO> GetHotel(int id)
        { 
            return await _context.Hotels.Select(x => new HotelDTO
            {
                ID = x.ID,
                Name = x.Name,
                StreetAddress = x.StreetAddress,
                City = x.City,
                State = x.State,
                Phone = x.Phone,
                Rooms = x.HotelRooms.Select(x => new HotelRoomDTO
                {
                    HotelID = x.HotelID,
                    RoomNumber = x.RoomNumber,
                    Rate = x.Rate,
                    PetFriendly = x.PetFriendly,
                    RoomID = x.RoomID,
                    Room = new RoomDTO
                    {
                        ID = x.Room.ID,
                        Name = x.Room.Name,
                        Layout = x.Room.Layout,
                        Amenities = x.Room.RoomAmenities.Select(x => new AmenityDTO
                        {
                            ID = x.AmenityID,
                            Name = x.Amenity.Name
                        }).ToList()
                    }
                }).ToList()
            }).FirstOrDefaultAsync(x => x.ID == id);           
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.ID == id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }
    }
}
