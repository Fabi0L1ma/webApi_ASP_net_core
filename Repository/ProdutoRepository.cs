using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context){}
     
        public IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria)
        {
            var produtos = GetAll().Where(c => c.CategoriaId == idCategoria);

            return produtos;
        }
    }
}
