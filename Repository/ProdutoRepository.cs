using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Context;
using WebApi.Models;
using WebApi.Paginacao;

namespace WebApi.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context){}

        public async Task<ListarPaginacao<Produto>> GetProdutosAsync(ProdutoParametros produtosParams)
        {
            var produtos = await GetAllAsync();

            var produtosOrdenados = produtos.OrderBy(p => p.ProdutoId).AsQueryable();

            var resultado = ListarPaginacao<Produto>.ToListaPagina(produtosOrdenados, produtosParams.NumeroPagina, produtosParams.TamanhoPagina);

            return resultado;       
        }

        public async Task<ListarPaginacao<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParams)
        {
            var produtos = await GetAllAsync();

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

            var produtosFiltrados = ListarPaginacao<Produto>.ToListaPagina(produtos.AsQueryable(), produtosFiltroParams.NumeroPagina, produtosFiltroParams.TamanhoPagina);

            return produtosFiltrados;
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int idCategoria)
        {
            var produtos = await GetAllAsync();

            var produtosOrdenados = produtos.Where(c => c.CategoriaId == idCategoria);

            return produtosOrdenados;
        }
    }
}
