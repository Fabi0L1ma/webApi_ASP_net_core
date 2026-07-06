using WebApi.Models;

namespace WebApi.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria);
    }
}
