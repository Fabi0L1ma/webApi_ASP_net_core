using WebApi.Models;
using WebApi.Paginacao;

namespace WebApi.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        ListaPagina<Categoria> GetCategorias(CategoriaParametros categoriaParams);
    }
}
