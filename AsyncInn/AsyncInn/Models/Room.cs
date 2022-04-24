using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Layout { get; set; }

        public List<HotelRoom> HotelRooms { get; set; }
        public List<RoomAmenity> RoomAmenities { get; set; }
    }
}