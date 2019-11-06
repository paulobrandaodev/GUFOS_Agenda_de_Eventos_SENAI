using backend.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using backend.Repositories;

// Adiciona a arvore de objetos 
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson


namespace backend.Controllers
{
    // Define a rota do controller, e diz que é um controller de API
    [Route("api/[controller]")] 
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : ControllerBase
    {
        UsuarioRepository _repositorio = new UsuarioRepository();

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuarios = await _repositorio.Listar();

            if(usuarios == null) {
                return NotFound();
            }

            return usuarios;
        }
        
        // GET: api/Usuario/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await _repositorio.BuscarPorID(id);

            if(usuario == null) {
                return NotFound();
            }

            return usuario;
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            try
            {
                await _repositorio.Salvar(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            
            return usuario;
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Usuario usuario)
        {
            if(id != usuario.UsuarioId){
                return BadRequest();
            }            
            
            try {
                await _repositorio.Alterar(usuario);
            } catch (DbUpdateConcurrencyException) {
                // Verfica se o objeto inserido existe no banco
                var usuario_valido = await _repositorio.BuscarPorID(id);

                if(usuario_valido == null) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/usuario/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id){
            var usuario = await _repositorio.BuscarPorID(id);

            if(usuario == null) {
                return NotFound();
            }            
            
            return usuario;
        }
    }
}