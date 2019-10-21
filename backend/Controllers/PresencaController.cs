using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresencaController : ControllerBase
    {
        GufosContext _context = new GufosContext();


        // GET: api/Presenca/
        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get()
        {
            var presencas = await _context.Presenca.Include("Evento").Include("Usuario").ToListAsync();

            if (presencas == null)
            {
                return NotFound();
            }

            return presencas;
        }

        // GET: api/Presenca/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Presenca>> Get(int id)
        {
            var presenca = await _context.Presenca.Include("Evento").Include("Usuario").FirstOrDefaultAsync(e => e.PresencaId == id);

            if (presenca == null)
            {
                return NotFound();
            }

            return presenca;
        }

        // POST: api/Presenca/
        [HttpPost]
        public async Task<ActionResult<Presenca>> Post(Presenca presenca)
        {
            try
            {
                await _context.AddAsync(presenca);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return presenca;
        }        


        // PUT: api/Presenca/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Presenca presenca)
        {
            if (id != presenca.PresencaId)
            {
                return BadRequest();
            }

            _context.Entry(presenca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var presenca_valido = await _context.Presenca.FindAsync(id);

                if (presenca_valido == null)
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

        // DELETE: api/Presenca/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Presenca>> Delete(int id)
        {
            var presenca = await _context.Presenca.FindAsync(id);
            if (presenca == null)
            {
                return NotFound();
            }

            _context.Presenca.Remove(presenca);
            await _context.SaveChangesAsync();

            return presenca;
        }



    }
}