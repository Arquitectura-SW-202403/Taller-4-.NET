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
    public class SpaceController : ControllerBase
    {
        private readonly GeneralContext _context;

        public SpaceController(GeneralContext context)
        {
            _context = context;
        }

        // GET: api/Space
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Space>>> Getspaces()
        {
            return await _context.spaces.Include(x => x.Zone).ToListAsync();
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

            return CreatedAtAction("GetSpace", new { id = space.id }, space);
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