using WebApi.Models;

namespace WebApi.Repository
{
    public interface IProdutoRepository
    {
        public Produto Create(Produto produto);
        public Produto Update(Produto produto);
        public Produto Delete(int id);
        public Produto GetById(int id);
        public IEnumerable<Produto> GetAll();
    }
}
