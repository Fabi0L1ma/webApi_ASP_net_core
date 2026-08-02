using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using WebApi.Paginacao;

namespace WebApi.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context){}

        public ListarPaginacao<Categoria> GetCategorias(CategoriaParametros categoriaParams)
        {
            var categorias = GetAll().OrderBy(c => c.CategoriaID).AsQueryable();

            var categoriasOrdenados = ListarPaginacao<Categoria>.ToListaPagina(categorias, categoriaParams.NumeroPagina, categoriaParams.TamanhoPagina);

            return categoriasOrdenados;
        }

        public ListarPaginacao<Categoria> GetCategoriasPorFiltroNome(CategoriasFiltroNome categoriasFiltroParams)
        {
            var categorias = GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(categoriasFiltroParams.Nome))
            {
                categorias = categorias.Where(c => c.Nome.Contains(categoriasFiltroParams.Nome)).OrderBy(c => c.Nome);
            }

            var categoriasFiltrados = ListarPaginacao<Categoria>.ToListaPagina(categorias, categoriasFiltroParams.NumeroPagina, categoriasFiltroParams.TamanhoPagina);

            return categoriasFiltrados;
        }
    }
}
