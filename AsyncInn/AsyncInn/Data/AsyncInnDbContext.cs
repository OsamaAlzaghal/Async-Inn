using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        // Creating tables.
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { ID = 1, Name = "Hotel Fairmont Amman", City = "Amman", Country = "Jordan", Phone = "065106000", State = "Amman", StreetAddress = "6 Beirut Street" },
                new Hotel { ID = 2, Name = "Geneva Hotel Amman", City = "Amman", Country = "Jordan", Phone = "065858100", State = "Amman", StreetAddress = "Abdallah Ghosheh Street" },
                new Hotel { ID = 3, Name = "Le Royal Hotel & Resorts Amman", City = "Amman", Country = "Jordan", Phone = "064603000", State = "Amman", StreetAddress = "Zahran Street"
                });

            modelBuilder.Entity<Room>().HasData(
                new Room { ID = 1, Layout = "Grey and Red Hotel Room", Name = "Solace And Comfort" },
                new Room { ID = 2, Layout = "Greek Inspired Hotel Room", Name = "Stylish Greek" },
                new Room { ID = 3, Layout = "Blue Boutique Hotel Room Layout", Name = "Blue Room"
                });

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity { ID = 1, Name = "Coffee Maker" },
                new Amenity { ID = 2, Name = "Ocean View" },
                new Amenity { ID = 3, Name = "Mini Bar"
                });

            // Keys added for join tables.
            modelBuilder.Entity<HotelRoom>().HasKey(x => new { x.HotelID, x.RoomID });
            modelBuilder.Entity<RoomAmenity>().HasKey(x => new { x.RoomID, x.AmenityID });
        }
    }
}