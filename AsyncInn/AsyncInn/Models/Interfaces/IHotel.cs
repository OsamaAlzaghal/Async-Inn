using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotel
    {
        Task<List<Hotel>> GetHotels();
        Task<Hotel> GetHotel(int id);
        Task<Hotel> Create(Hotel hotel);
        Task Delete(int id);
        Task<Hotel> UpdateHotel(int id, Hotel hotel);
    }
}