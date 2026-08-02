using WebApi.Models;
using WebApi.Paginacao;

namespace WebApi.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        ListarPaginacao<Produto> GetProdutos(ProdutoParametros produtosParams);
        ListarPaginacao<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco);
        IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria);
    }
}
