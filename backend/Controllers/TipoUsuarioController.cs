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
    public class TipoUsuarioController : ControllerBase
    {
        TipoUsuarioRepository _repositorio = new TipoUsuarioRepository();

        // GET: api/TipoUsuario
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get()
        {
            var tipos_usuario = await _repositorio.Listar();

            if(tipos_usuario == null) {
                return NotFound();
            }

            return tipos_usuario;
        }
        
        // GET: api/TipoUsuario/2
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get(int id)
        {
            var tipo_usuario = await _repositorio.BuscarPorID(id);

            if(tipo_usuario == null) {
                return NotFound();
            }

            return tipo_usuario;
        }

        // POST: api/TipoUsuario
        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> Post(TipoUsuario tipo_usuario)
        {
            try
            {
                await _repositorio.Salvar(tipo_usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            
            return tipo_usuario;
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, TipoUsuario tipo_usuario)
        {
            if(id != tipo_usuario.TipoUsuarioId){
                return BadRequest();
            }
            
            try {
                await _repositorio.Alterar(tipo_usuario);  
            } catch (DbUpdateConcurrencyException) {
                // Verfica se o objeto inserido existe no banco
                var tipo_usuario_valido = await _repositorio.BuscarPorID(id);

                if(tipo_usuario_valido == null) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/tipo_usuario/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoUsuario>> Delete(int id){
            var tipo_usuario = await _repositorio.BuscarPorID(id);

            if(tipo_usuario == null) {
                return NotFound();
            }          
            
            return tipo_usuario;
        }
    }
}