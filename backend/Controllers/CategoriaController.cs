using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    // Definimos nossa rota do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        
        //GufosContext _contexto = new GufosContext();
        CategoriaRepository _repositorio = new CategoriaRepository();

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get(){

            var categorias = await _repositorio.Listar();

            if( categorias == null ){
                return NotFound();
            }

            return categorias;
        }

        // GET: api/Categoria/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Get(int id){

            // FindAsync = procura algo específico no banco
            var categoria = await _repositorio.BuscarPorID(id);

            if( categoria == null ){
                return NotFound();
            }

            return categoria;
        }

        // POST api/Categoria
        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria){

            try{

                await _repositorio.Salvar(categoria);
            
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return categoria;
        }

        // PUT api/categoria/4
        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, Categoria categoria ){

            // Se o Id do objeto não existir
            // ele retorna erro 400
            if(id != categoria.CategoriaId){
                return BadRequest();
            }            

            try{

                await _repositorio.Alterar(categoria);

            }catch(DbUpdateConcurrencyException){

                // Verificamos se o objeto inserido realmente existe no banco
                var categoria_valido = await _repositorio.BuscarPorID(id);

                if(categoria_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
                
            }
            // NoContent = Retorna 204, sem nada
            return NoContent();
        }

        // DELETE api/categoria/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id){

            var categoria = await _repositorio.BuscarPorID(id);
            if(categoria == null){
                return NotFound();
            }

            await _repositorio.Excluir(categoria);

            return categoria;
        }  




    }
}