using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class Amenity
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<RoomAmenity> RoomAmenities { get; set; }
    }
}