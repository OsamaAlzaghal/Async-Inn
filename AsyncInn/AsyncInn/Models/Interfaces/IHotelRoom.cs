using AsyncInn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelRoom
    {

        Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId);

        Task<HotelRoom> AddRoomToHotel(int hotelId, HotelRoomDTO room);

        Task<HotelRoomDTO> RoomDetails(int hotelId, int roomNumber);

        Task<HotelRoom> UpdateRoomDetails(int hotelId, int roomNumber, HotelRoomDTO hotelRoom);

        Task DeleteRoomFromHotel(int hotelId, int roomNumber);
    }
}
