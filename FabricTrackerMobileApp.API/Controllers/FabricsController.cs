using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FabricTrackerMobileApp.API.Data;
using FabricTrackerMobileApp.API.Models;

namespace FabricTrackerMobileApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricsController : ControllerBase
    {
        private readonly FabricTrackerDbContext _context;

        public FabricsController(FabricTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/Fabrics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fabrics>>> GetFabrics()
        {
            return await _context.Fabrics.ToListAsync();
        }

        // GET: api/Fabrics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fabrics>> GetFabrics(int id)
        {
            var fabrics = await _context.Fabrics.FindAsync(id);

            if (fabrics == null)
            {
                return NotFound();
            }

            return fabrics;
        }

        // PUT: api/Fabrics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFabrics(int id, Fabrics fabrics)
        {
            if (id != fabrics.Id)
            {
                return BadRequest();
            }

            _context.Entry(fabrics).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FabricsExists(id))
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

        // POST: api/Fabrics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Fabrics>> PostFabrics(Fabrics fabrics)
        {
            _context.Fabrics.Add(fabrics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFabrics", new { id = fabrics.Id }, fabrics);
        }

        // DELETE: api/Fabrics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fabrics>> DeleteFabrics(int id)
        {
            var fabrics = await _context.Fabrics.FindAsync(id);
            if (fabrics == null)
            {
                return NotFound();
            }

            _context.Fabrics.Remove(fabrics);
            await _context.SaveChangesAsync();

            return fabrics;
        }

        private bool FabricsExists(int id)
        {
            return _context.Fabrics.Any(e => e.Id == id);
        }
    }
}
