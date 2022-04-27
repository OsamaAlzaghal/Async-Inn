using AsyncInn.Data;
using AsyncInn.Models.DTOs;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<HotelRoom> AddRoomToHotel(int hotelId, HotelRoomDTO room)
        {
            var hotelRoom = new HotelRoom
            {
                HotelID = hotelId,
                RoomID = room.RoomID,
                RoomNumber = room.RoomNumber,
                Rate = room.Rate,
                PetFriendly = room.PetFriendly
            };

            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }


        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            return await _context.HotelRooms.Where(x => x.HotelID == hotelId).Select(x => new HotelRoomDTO
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
            }).ToListAsync();
        }

        public async Task<HotelRoomDTO> RoomDetails(int hotelId, int roomNumber)
        {
            return await _context.HotelRooms
                .Where(x => x.HotelID == hotelId & x.RoomNumber == roomNumber)
                .Select(x => new HotelRoomDTO
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

                }).FirstOrDefaultAsync();
        }

        // Fixed.
        public async Task<HotelRoom> UpdateRoomDetails(int hotelId, int roomNumber, HotelRoomDTO hotelRoom)
        {
            var oldhotelRoom = await _context.HotelRooms
                                          .Where(x => x.HotelID == hotelId & x.RoomNumber == roomNumber)
                                          .FirstOrDefaultAsync();

            oldhotelRoom.PetFriendly = hotelRoom.PetFriendly;
            oldhotelRoom.Rate = hotelRoom.Rate;
            _context.Entry(oldhotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldhotelRoom;
        }
        public async Task DeleteRoomFromHotel(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms
                                          .Where(x => x.HotelID == hotelId & x.RoomNumber == roomNumber)
                                          .FirstOrDefaultAsync();

            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
