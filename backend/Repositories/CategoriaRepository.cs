using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class CategoriaRepository : ICategoria
    {
        public async Task<Categoria> Alterar(Categoria categoria)
        {
            using(GufosContext _contexto = new GufosContext()){
                _contexto.Entry(categoria).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            return categoria;
        }

        public async Task<Categoria> BuscarPorID(int id)
        {
            using(GufosContext _contexto = new GufosContext()){
                return await _contexto.Categoria.FindAsync(id);
            }            
        }

        public async Task<Categoria> Excluir(Categoria categoria)
        {
            using(GufosContext _contexto = new GufosContext()){
                _contexto.Categoria.Remove(categoria);
                await _contexto.SaveChangesAsync();
            }
            return categoria;
        }

        public async Task<List<Categoria>> Listar()
        {
            using(GufosContext _contexto = new GufosContext()){
                return await  _contexto.Categoria.ToListAsync();
            }    
        }

        public async Task<Categoria> Salvar(Categoria categoria)
        {
            using(GufosContext _contexto = new GufosContext()){
                await _contexto.AddAsync(categoria);
                await _contexto.SaveChangesAsync();
            }

            return categoria;
        }
    }
}