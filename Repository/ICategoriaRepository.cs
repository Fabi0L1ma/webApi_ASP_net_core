using WebApi.Models;

namespace WebApi.Repository
{
    public interface ICategoriaRepository
    {
        public IEnumerable<Categoria> GetCategorias();
        public Categoria GetCategoriaPorId(int? id);
        public Categoria Create(Categoria categoria);
        public Categoria Update(Categoria categoria);
        public Categoria Delete(int? id);
    }
}
