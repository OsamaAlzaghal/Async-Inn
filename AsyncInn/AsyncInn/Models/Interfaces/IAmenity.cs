using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenity
    {
        Task<List<Amenity>> GetAmenities();
        Task<Amenity> GetAmenity(int id);
        Task<Amenity> Create(Amenity amenity);
        Task Delete(int id);
        Task<Amenity> UpdateAmenity(int id, Amenity amenity);
    }
}
