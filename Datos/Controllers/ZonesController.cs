using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos.Models;
using Entidades;

namespace Datos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonesController : ControllerBase
    {
        private readonly GeneralContext _context;

        public ZonesController(GeneralContext context)
        {
            _context = context;
        }

        // GET: api/Zones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zone>>> Getzones()
        {
            return await _context.zones.Include(x => x.Espacios).ToListAsync();
        }

        // GET: api/Zones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zone>> GetZone(int id)
        {
            var zone = await _context.zones.FindAsync(id);

            if (zone == null)
            {
                return NotFound();
            }

            return zone;
        }

        // PUT: api/Zones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZone(int id, Zone zone)
        {
            if (id != zone.id)
            {
                return BadRequest();
            }

            _context.Entry(zone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Zones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zone>> PostZone(Zone zone)
        {
            _context.zones.Add(zone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZone", new { id = zone.id }, zone);
        }

        // DELETE: api/Zones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZone(int id)
        {
            var zone = await _context.zones.FindAsync(id);
            if (zone == null)
            {
                return NotFound();
            }

            _context.zones.Remove(zone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZoneExists(int id)
        {
            return _context.zones.Any(e => e.id == id);
        }
    }
}
