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

        public ListaPagina<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParams)
        {
            var produtos = GetAll().AsQueryable();

            if(produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
            {
                if(produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => (decimal) p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => (decimal) p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => (decimal)p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
            }

            var produtosFiltrados = ListaPagina<Produto>.ToListaPagina(produtos, produtosFiltroParams.NumeroPagina, produtosFiltroParams.TamanhoPagina);

            return produtosFiltrados;
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria)
        {
            var produtos = GetAll().Where(c => c.CategoriaId == idCategoria);

            return produtos;
        }
    }
}
