using AsyncInn.Data;
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

        public async Task<Amenity> Create(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return amenity;
        }
        public async Task<List<Amenity>> GetAmenities()
        {
            //var amenities = await _context.Amenities.ToListAsync();
            //return amenities;

            var amenities = await _context.Amenities
                                      .Include(x => x.RoomAmenities)
                                      .ThenInclude(c => c.Amenity)
                                      .ToListAsync();
            return amenities;
        }
        public async Task<Amenity> GetAmenity(int id)
        {
            //// The system knows we have a primary key and will use it
            //Amenity amenity = await _context.Amenities.FindAsync(id);
            //return amenity;

            var amenity = await _context.Amenities.Where(x => x.ID == id)
                                            .Include(x => x.RoomAmenities)
                                            .ThenInclude(c => c.Room)
                                            .FirstOrDefaultAsync();
            return amenity;
        }

        public async Task Delete(int id)
        {
            Amenity amenity = await GetAmenity(id);
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
