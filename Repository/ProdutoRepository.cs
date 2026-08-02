using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Context;
using WebApi.Models;
using WebApi.Paginacao;

namespace WebApi.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context){}

        public ListaPagina<Produto> GetProdutos(ProdutoParametros produtosParams)
        {
            var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();

            var produtosOrdenados = ListaPagina<Produto>.ToListaPagina(produtos, produtosParams.NumeroPagina, produtosParams.TamanhoPagina);

            return produtosOrdenados;       
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria)
        {
            var produtos = GetAll().Where(c => c.CategoriaId == idCategoria);

            return produtos;
        }
    }
}
