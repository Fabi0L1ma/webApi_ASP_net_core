using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context) 
        {
            _context = context;
        }
        public Produto Create(Produto produto)
        {
            if(produto is null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            _context.Produtos.Add(produto);
         
            _context.SaveChanges();

            return produto;
        }

        public Produto Delete(int id)
        {
            var produto = _context.Produtos.Find(id);

            if(produto is null)
            {
                throw new Exception("Produto não encontrado");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return produto;
        }

        public IEnumerable<Produto> GetAll()
        {
            var produtos = _context.Produtos.ToList();

            if(produtos is null)
            {
                throw new Exception("Produto não encontrado.");
            }

            return produtos;
        }

        public Produto GetById(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if(produto is null)
            {
                throw new Exception("Produto não encontrado.");
            }

            return produto;
        }

        public Produto Update(Produto produto)
        {
            if(produto is null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            _context.Produtos.Update(produto);

            _context.SaveChanges();

            return produto;
        }
    }
}
