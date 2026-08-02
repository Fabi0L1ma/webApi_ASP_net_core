using WebApi.Models;
using WebApi.Paginacao;

namespace WebApi.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        ListaPagina<Produto> GetProdutos(ProdutoParametros produtosParams);
        IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria);
    }
}
