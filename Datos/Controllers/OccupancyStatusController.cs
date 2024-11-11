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
    public class OccupancyStatusController : ControllerBase
    {
        private readonly GeneralContext _context;

        public OccupancyStatusController(GeneralContext context)
        {
            _context = context;
        }

        // GET: api/OccupancyStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OccupancyStatus>>> GetoccupancyStatus()
        {
            return await _context.occupancyStatus.ToListAsync();
        }

        // GET: api/OccupancyStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OccupancyStatus>> GetOccupancyStatus(long id)
        {
            var occupancyStatus = await _context.occupancyStatus.FindAsync(id);

            if (occupancyStatus == null)
            {
                return NotFound();
            }

            return occupancyStatus;
        }

        // PUT: api/OccupancyStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOccupancyStatus(long id, OccupancyStatus occupancyStatus)
        {
            if (id != occupancyStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(occupancyStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OccupancyStatusExists(id))
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

        // POST: api/OccupancyStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OccupancyStatus>> PostOccupancyStatus(OccupancyStatus occupancyStatus)
        {
            _context.occupancyStatus.Add(occupancyStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOccupancyStatus", new { id = occupancyStatus.Id }, occupancyStatus);
        }

        // DELETE: api/OccupancyStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOccupancyStatus(long id)
        {
            var occupancyStatus = await _context.occupancyStatus.FindAsync(id);
            if (occupancyStatus == null)
            {
                return NotFound();
            }

            _context.occupancyStatus.Remove(occupancyStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OccupancyStatusExists(long id)
        {
            return _context.occupancyStatus.Any(e => e.Id == id);
        }
    }
}
