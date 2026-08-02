using WebApi.Models;
using WebApi.Paginacao;

namespace WebApi.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        ListaPagina<Produto> GetProdutos(ProdutoParametros produtosParams);
        ListaPagina<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco);
        IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria);
    }
}
