using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class LocalizacaoRepository : ILocalizacao
    {
        public async Task<Localizacao> Alterar(Localizacao localizacao)
        {
            using(GufosContext _contexto = new GufosContext()){
                _contexto.Entry(localizacao).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            return localizacao;
        }

        public async Task<Localizacao> BuscarPorID(int id)
        {
            using(GufosContext _contexto = new GufosContext()){
               return await _contexto.Localizacao.FindAsync(id);            
            }
        }

        public async Task<Localizacao> Excluir(Localizacao localizacao)
        {
            using(GufosContext _contexto = new GufosContext()){
                _contexto.Localizacao.Remove(localizacao);
                await _contexto.SaveChangesAsync();
                
                return localizacao;
            }
        }

        public async Task<List<Localizacao>> Listar()
        {
            using(GufosContext _contexto = new GufosContext()){
                return await _contexto.Localizacao.Include("Evento").ToListAsync();
            }
        }

        public async Task<Localizacao> Salvar(Localizacao localizacao)
        {
            using(GufosContext _contexto = new GufosContext()){
                await _contexto.AddAsync(localizacao);
                await _contexto.SaveChangesAsync();
            }
            return localizacao;
        }
    }
}