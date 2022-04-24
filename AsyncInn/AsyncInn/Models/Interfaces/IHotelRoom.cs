using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelRoom
    {

        Task<Hotel> GetHotelRooms(int hotelId);

        Task<HotelRoom> AddRoomToHotel(int hotelId, int roomId);

        Task<Room> RoomDetails(int hotelId, int roomId);

        Task<Room> UpdateRoomDetails(int hotelId, int roomId, Room room);

        Task DeleteRoomFromHotel(int hotelId, int roomId);
    }
}
