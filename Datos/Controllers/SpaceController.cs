using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos.Context;
using Entidades;

namespace Datos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceController : ControllerBase
    {
        private readonly GeneralContext _context;

        public SpaceController(GeneralContext context)
        {
            _context = context;
        }

        // GET: api/Space
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Space>>> GetSpaces()
        {
            return new JsonResult(
                new {
                    results = await _context.spaces.ToListAsync()
                }
            );
        }

        // GET: api/Space/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Space>> GetSpace(long id)
        {
            var space = await _context.spaces.FindAsync(id);

            if (space == null)
            {
                return BadRequest();
            }
            return space;
        }

        // PUT: api/Space/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpace(long id, Space space)
        {
            if (id != space.id)
            {
                return BadRequest();
            }

            _context.Entry(space).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpaceExists(id))
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

        // POST: api/Space
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Space>> PostSpace(Space space)
        {
            _context.spaces.Add(space);
            await _context.SaveChangesAsync();

            for (int i = 6; i <= 18; i+=2) {
                _context.occupancy_status.Add(
                    new OccupancyStatus {
                        owner = "",
                        space_id = space.id,
                        start_time = i,
                        end_time = i+2,
                        status = "available"
                    }
                );
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Space/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpace(long id)
        {
            var space = await _context.spaces.FindAsync(id);
            if (space == null)
            {
                return NotFound();
            }

            var occupance = await _context.occupancy_status.Where(x => x.space_id == id).ToListAsync();

            foreach (var occ in occupance) {
                _context.occupancy_status.Remove(occ); 
            }

            _context.spaces.Remove(space);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpaceExists(long id)
        {
            return _context.spaces.Any(e => e.id == id);
        }
    }
}
