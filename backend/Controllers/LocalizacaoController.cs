using backend.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    // Define a rota do controller, e diz que é um controller de API
    [Route("api/[controller]")] 
    [ApiController]
    [Authorize]
    public class LocalizacaoController : ControllerBase
    {
        LocalizacaoRepository _repositorio = new LocalizacaoRepository();

        // GET: api/Localizacao
        [HttpGet]
        public async Task<ActionResult<List<Localizacao>>> Get()
        {
            var localizacoes = await _repositorio.Listar();

            if(localizacoes == null) {
                return NotFound();
            }

            return localizacoes;
        }
        
        // GET: api/Localizacao/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Localizacao>> Get(int id)
        {
            var localizacao = await _repositorio.BuscarPorID(id);

            if(localizacao == null) {
                return NotFound();
            }

            return localizacao;
        }

        // POST: api/Localizacao
        [HttpPost]
        public async Task<ActionResult<Localizacao>> Post(Localizacao localizacao)
        {
            try
            {
                await _repositorio.Salvar(localizacao);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            
            return localizacao;
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Localizacao localizacao)
        {
            if(id != localizacao.LocalizacaoId){
                return BadRequest();
            }

            try {
                await _repositorio.Alterar(localizacao);
            } catch (DbUpdateConcurrencyException) {
                // Verfica se o objeto inserido existe no banco
                var localizacao_valido = await _repositorio.BuscarPorID(id);

                if(localizacao_valido == null) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/localizacao/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Localizacao>> Delete(int id){
            var localizacao = await _repositorio.BuscarPorID(id);

            if(localizacao == null) {
                return NotFound();
            }           
            
            return localizacao;
        }
    }
}