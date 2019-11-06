using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class EventoRepository : IEvento
    {
        public async Task<Evento> Alterar(Evento evento)
        {
            using(GufosContext _contexto = new GufosContext()) {
                _contexto.Entry(evento).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            return evento;
        }

        public async Task<Evento> BuscarPorID(int id)
        {
            using(GufosContext _contexto = new GufosContext()) {
                return await _contexto.Evento.Include("Categoria").Include("Localizacao").FirstOrDefaultAsync(e => e.EventoId == id);
            }
        }

        public async Task<Evento> Excluir(Evento evento)
        {
            using(GufosContext _contexto = new GufosContext()) {
                _contexto.Evento.Remove(evento);
                await _contexto.SaveChangesAsync();
            }
            return evento;
        }

        public async Task<List<Evento>> Listar()
        {
            using(GufosContext _contexto = new GufosContext()) {
                return await _contexto.Evento.Include("Categoria").Include("Localizacao").ToListAsync();
            }
        }

        public async Task<Evento> Salvar(Evento evento)
        {
            using(GufosContext _contexto = new GufosContext()) {
                await _contexto.AddAsync(evento);
                await _contexto.SaveChangesAsync();
                return evento;
            }
        }
    }
}