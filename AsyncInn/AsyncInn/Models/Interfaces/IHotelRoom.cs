using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelRoom
    {

        Task<List<HotelRoom>> GetHotelRooms(int hotelId);

        Task<HotelRoom> AddRoomToHotel(int hotelId, int roomId, int roomNumber);

        Task<HotelRoom> RoomDetails(int hotelId, int roomNumber);

        Task<HotelRoom> UpdateRoomDetails(int hotelId, int roomNumber, HotelRoom hotelRoom);

        Task DeleteRoomFromHotel(int hotelId, int roomNumber);
    }
}
