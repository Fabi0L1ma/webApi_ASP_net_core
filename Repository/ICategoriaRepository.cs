using WebApi.Models;
using WebApi.Paginacao;

namespace WebApi.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<ListarPaginacao<Categoria>> GetCategoriasAsync(CategoriaParametros categoriaParams);
        Task<ListarPaginacao<Categoria>> GetCategoriasPorFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroNome);

    }
}
