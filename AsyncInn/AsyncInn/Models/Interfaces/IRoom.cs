using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IRoom
    {
        Task<List<Room>> GetRooms();
        Task<Room> GetRoom(int id);
        Task<Room> Create(Room room);
        Task Delete(int id);
        Task<Room> UpdateRoom(int id, Room room);

        // New methods.
        Task AddAmenityToRoom(int roomId, int amenityId);
        Task RemoveAmentityFromRoom(int roomId, int amenityId);
    }
}
