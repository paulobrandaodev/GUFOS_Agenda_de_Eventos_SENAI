using backend.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;

// Adiciona a arvore de objetos 
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson


namespace backend.Controllers
{
    // Define a rota do controller, e diz que é um controller de API
    [Route("api/[controller]")] 
    [ApiController]
    [Authorize]
    public class EventoController : ControllerBase
    {

        EventoRepository _repositorio = new EventoRepository();

        // GET: api/Evento
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get()
        {
            var eventos = await _repositorio.Listar();

            if(eventos == null) {
                return NotFound();
            }

            return eventos;
        }
        
        // GET: api/Evento/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id)
        {
            var evento = await _repositorio.BuscarPorID(id);

            if(evento == null) {
                return NotFound();
            }

            return evento;
        }

        // POST: api/Evento
        [HttpPost]
        public async Task<ActionResult<Evento>> Post(Evento evento)
        {
            try
            {
                await _repositorio.Salvar(evento);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return evento;          
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Evento evento)
        {
            if(id != evento.EventoId){
                return BadRequest();
            }                     
            
            try {
                await _repositorio.Alterar(evento);
            } catch (DbUpdateConcurrencyException) {
                // Verfica se o objeto inserido existe no banco
                var evento_valido = await _repositorio.BuscarPorID(id);

                if(evento_valido == null) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/evento/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evento>> Delete(int id){
            var evento = await _repositorio.BuscarPorID(id);

            if(evento == null) {
                return NotFound();
            }           

            await _repositorio.Excluir(evento);
            
            return evento;
        }
    }
}