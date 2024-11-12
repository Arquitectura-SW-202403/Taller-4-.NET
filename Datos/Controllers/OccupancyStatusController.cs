using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos.Context;
using Entidades;
using Datos.Model;

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
            return new JsonResult(
                new {
                    results = await _context.occupancy_status.ToListAsync()
                }
            );
        }

        // GET api/OccupancyStatus/spaceId?start=?&end=?
        [HttpGet("{spaceId}")]
        public async Task<ActionResult<IEnumerable<Space>>> GetOccupanciesByRange(
            long spaceId, 
            [FromQuery(Name = "start")] long start, 
            [FromQuery(Name = "end")] long end
        )
        {
            List<OccupancyStatus> result = await _context.occupancy_status.Where(
                x => x.space_id == spaceId && x.start_time >= start && x.end_time <= end
            ).ToListAsync();


            var obj = new {
                result
            };

            return Ok(obj);
        }

        // POST api/OccupancyStatus/block/spaceId
        [HttpPost("block/{spaceId}")]
        public async Task<IActionResult> BlockRange
        (
            long spaceId,
            BlockRangeDTO block
        )
        {
            if (block.start % 2 != 0 || block.end % 2 != 0) return BadRequest();
            for (int i = block.start; i < block.end; i+= 2) {
                var range = await _context.occupancy_status.Where(x => x.start_time == i && x.space_id == spaceId).ToListAsync();

                if (range.First().status == "occupied") return BadRequest();

                range.First().owner = block.owner;
                range.First().status = "occupied";
            }

            try {
                await _context.SaveChangesAsync();            
            } catch (DbUpdateConcurrencyException) {
                return UnprocessableEntity();
            }

            return NoContent();
        }

        // POST api/OccupancyStatus/block/spaceId
        [HttpPost("free/{spaceId}")]
        public async Task<IActionResult> FreeRange
        (
            long spaceId,
            BlockRangeDTO block
        )
        {
            if (block.start % 2 != 0 || block.end % 2 != 0) return BadRequest();
            for (int i = block.start; i < block.end; i+= 2) {
                var range = await _context.occupancy_status.Where(x => x.start_time == i && x.space_id == spaceId).ToListAsync();
                range.First().owner = "";
                range.First().status = "available";
            }

            try {
                await _context.SaveChangesAsync();            
            } catch (DbUpdateConcurrencyException) {
                return UnprocessableEntity();
            }

            return NoContent();
        }


        // PUT: api/OccupancyStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOccupancyStatus(long id, OccupancyStatus occupancyStatus)
        {
            if (id != occupancyStatus.id)
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
            _context.occupancy_status.Add(occupancyStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOccupancyStatus", new { id = occupancyStatus.id }, occupancyStatus);
        }

        // DELETE: api/OccupancyStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOccupancyStatus(long id)
        {
            var occupancyStatus = await _context.occupancy_status.FindAsync(id);
            if (occupancyStatus == null)
            {
                return NotFound();
            }

            _context.occupancy_status.Remove(occupancyStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OccupancyStatusExists(long id)
        {
            return _context.occupancy_status.Any(e => e.id == id);
        }
    }
}
