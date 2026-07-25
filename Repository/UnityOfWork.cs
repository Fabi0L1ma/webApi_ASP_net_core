using WebApi.Context;

namespace WebApi.Repository
{
    public class UnityOfWork : IUnityOfWork
    {
        private ICategoriaRepository _categoriaRepository;

        private IProdutoRepository _produtoRepository;

        public AppDbContext _context;

        public UnityOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return this._categoriaRepository ?? new CategoriaRepository(this._context);
            }
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return this._produtoRepository ?? new ProdutoRepository(this._context);
            }
        }

        public void Commit()
        {
            this._context.SaveChanges();
        }
        
        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
