using WebApi.Models;
using WebApi.Paginacao;

namespace WebApi.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        ListarPaginacao<Categoria> GetCategorias(CategoriaParametros categoriaParams);
        ListarPaginacao<Categoria> GetCategoriasPorFiltroNome(CategoriasFiltroNome categoriasFiltroNome);

    }
}
