using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.Interfaces;
using AsyncInn.Models.DTOs;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotel;

        public HotelsController(IHotel hotel)
        {
            _hotel = hotel;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels() // GetHotels.
        {
            var hotels = await _hotel.GetHotels();
            return Ok(hotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id) // GetHotel.
        {
            HotelDTO hotel = await _hotel.GetHotel(id);
            return Ok(hotel);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel) // UpdateHotel.
        {
            if (id != hotel.ID)
            {
                return BadRequest();
            }
            var modifiedHotel = await _hotel.UpdateHotel(id, hotel);
            return Ok(modifiedHotel);

            //_context.Entry(hotel).State = EntityState.Modified;
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!HotelExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel) // CreateHotel.
        {
            Hotel newHotel = await _hotel.Create(hotel);
            return Ok(newHotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id) // Delete.
        {
            await _hotel.Delete(id);
            return NoContent();

            //var hotel = await _context.Hotels.FindAsync(id);
            //if (hotel == null)
            //{
            //    return NotFound();
            //}
            //_context.Hotels.Remove(hotel);
            //await _context.SaveChangesAsync();
            //return NoContent();
        }

        //private bool HotelExists(int id)
        //{
        //    return _context.Hotels.Any(e => e.ID == id);
        //}
    }
}
