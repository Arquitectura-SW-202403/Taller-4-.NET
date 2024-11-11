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
    public class SpaceXStatusController : ControllerBase
    {
        private readonly GeneralContext _context;

        public SpaceXStatusController(GeneralContext context)
        {
            _context = context;
        }

        // GET: api/SpaceXStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpaceXStatus>>> GetspaceXStatus()
        {
            return await _context.space_x_status.Include(x => x.Space).Include(x  => x.OccupancyStatus).ToListAsync();
        }

        // GET: api/SpaceXStatus/5/4
        [HttpGet("{idf}/{ids}")]
        public async Task<ActionResult<SpaceXStatus>> GetSpaceXStatus(long idf, long ids)
        {
            var spaceXStatus = await _context.space_x_status.FindAsync(idf, ids);

            if (spaceXStatus == null)
            {
                return BadRequest();
            }

            return spaceXStatus;
        }


        // POST: api/SpaceXStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpaceXStatus>> PostSpaceXStatus(SpaceXStatus spaceXStatus)
        {
            _context.space_x_status.Add(spaceXStatus);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SpaceXStatusExists(spaceXStatus.space_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSpaceXStatus", new { id = spaceXStatus.space_id }, spaceXStatus);
        }

        // DELETE: api/SpaceXStatus/5
        [HttpDelete("{idf}/{ids}")]
        public async Task<IActionResult> DeleteSpaceXStatus(long idf, long ids)
        {
            var spaceXStatus = await _context.space_x_status.FindAsync(idf, ids);
            if (spaceXStatus == null)
            {
                return NotFound();
            }

            _context.space_x_status.Remove(spaceXStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpaceXStatusExists(long id)
        {
            return _context.space_x_status.Any(e => e.space_id == id);
        }
    }
}
