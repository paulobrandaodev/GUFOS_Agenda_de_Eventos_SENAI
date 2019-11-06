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
    public class PresencaController : ControllerBase
    {
        PresencaRepository _repositorio = new PresencaRepository();

        // GET: api/Presenca
        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get()
        {
            var presencas = await _repositorio.Listar();

            if(presencas == null) {
                return NotFound();
            }

            return presencas;
        }
        
        // GET: api/Presenca/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Presenca>> Get(int id)
        {
            var presenca = await _repositorio.BuscarPorID(id);

            if(presenca == null) {
                return NotFound();
            }

            return presenca;
        }

        // POST: api/Presenca
        [HttpPost]
        public async Task<ActionResult<Presenca>> Post(Presenca presenca)
        {
            try
            {
                await _repositorio.Salvar(presenca);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            
            return presenca;
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Presenca presenca)
        {
            if(id != presenca.PresencaId){
                return BadRequest();
            }

            try {
                await _repositorio.Alterar(presenca);
            } catch (DbUpdateConcurrencyException) {
                var presenca_valido = await _repositorio.BuscarPorID(id);

                if(presenca_valido == null) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/presenca/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Presenca>> Delete(int id){
            var presenca = await _repositorio.BuscarPorID(id);

            if(presenca == null) {
                return NotFound();
            }            
            
            return presenca;
        }
    }
}