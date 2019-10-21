using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        GufosContext _context = new GufosContext();


        // GET: api/TipoUsuario/
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get()
        {
            var tipoUsuarios = await _context.TipoUsuario.ToListAsync();

            if (tipoUsuarios == null)
            {
                return NotFound();
            }

            return tipoUsuarios;
        }

        // GET: api/TipoUsuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get(int id)
        {
            var tipoUsuario = await _context.TipoUsuario.FindAsync(id);

            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return tipoUsuario;
        }

        // POST: api/TipoUsuario/
        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> Post(TipoUsuario tipoUsuario)
        {
            try
            {
                await _context.AddAsync(tipoUsuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return tipoUsuario;
        }        


        // PUT: api/TipoUsuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, TipoUsuario tipoUsuario)
        {
            if (id != tipoUsuario.TipoUsuarioId)
            {
                return BadRequest();
            }

            _context.Entry(tipoUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var tipoUsuario_valido = await _context.TipoUsuario.FindAsync(id);

                if (tipoUsuario_valido == null)
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

        // DELETE: api/TipoUsuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoUsuario>> Delete(int id)
        {
            var tipoUsuario = await _context.TipoUsuario.FindAsync(id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            _context.TipoUsuario.Remove(tipoUsuario);
            await _context.SaveChangesAsync();

            return tipoUsuario;
        }



    }
}