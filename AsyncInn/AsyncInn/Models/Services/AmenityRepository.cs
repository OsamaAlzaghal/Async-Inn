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
    public class AmenityRepository : IAmenity
    {
        private readonly AsyncInnDbContext _context;

        public AmenityRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Amenity> Create(AmenityDTO amenityDTO)
        {
            Amenity newAmenity = new Amenity
            {
                Name = amenityDTO.Name
            };

            _context.Entry(newAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return newAmenity;
        }
        public async Task<List<AmenityDTO>> GetAmenities()
        {
            return await _context.Amenities.Select(x => new AmenityDTO
            {
                ID = x.ID,
                Name = x.Name
            }).ToListAsync();
        }
        public async Task<AmenityDTO> GetAmenity(int id)
        {
            return await _context.Amenities.Select(x => new AmenityDTO
            {
                ID = x.ID,
                Name = x.Name
            }).FirstOrDefaultAsync( x => x.ID == id);
        }

        public async Task Delete(int id)
        {

            Amenity amenity = await _context.Amenities.FirstOrDefaultAsync(x => x.ID == id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        public async Task<Amenity> UpdateAmenity(int id, Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenity;
        }
    }
}
