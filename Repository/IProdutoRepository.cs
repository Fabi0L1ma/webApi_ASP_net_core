using WebApi.Models;
using WebApi.Paginacao;

namespace WebApi.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<ListarPaginacao<Produto>> GetProdutosAsync(ProdutoParametros produtosParams);
        Task<ListarPaginacao<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPreco);
        Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int idCategoria);
    }
}
