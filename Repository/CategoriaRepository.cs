using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using WebApi.Paginacao;

namespace WebApi.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context){}

        public async Task<ListarPaginacao<Categoria>> GetCategoriasAsync(CategoriaParametros categoriaParams)
        {
            var categorias = await GetAllAsync();

            var categoriaOrdenadas = categorias.OrderBy(c => c.CategoriaID).AsQueryable();

            var resultado = ListarPaginacao<Categoria>.ToListaPagina(categoriaOrdenadas, categoriaParams.NumeroPagina, categoriaParams.TamanhoPagina);

            return resultado;
        }

        public async Task<ListarPaginacao<Categoria>> GetCategoriasPorFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroParams)
        {
            var categorias = await GetAllAsync();

            if (!string.IsNullOrEmpty(categoriasFiltroParams.Nome))
            {
                categorias = categorias.Where(c => c.Nome.Contains(categoriasFiltroParams.Nome)).OrderBy(c => c.Nome);
            }

            var categoriasFiltrados = ListarPaginacao<Categoria>.ToListaPagina(categorias.AsQueryable(), categoriasFiltroParams.NumeroPagina, categoriasFiltroParams.TamanhoPagina);

            return categoriasFiltrados;
        }
    }
}
