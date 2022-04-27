using AsyncInn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenity
    {
        Task<List<AmenityDTO>> GetAmenities();
        Task<AmenityDTO> GetAmenity(int id);
        Task<Amenity> Create(AmenityDTO amenity);
        Task Delete(int id);
        Task<Amenity> UpdateAmenity(int id, Amenity amenity);
    }
}
