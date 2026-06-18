using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Categoria Create(Categoria categoria)
        {
            if(categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            _context.Categorias.Add(categoria);

            _context.SaveChanges();

            return categoria;
        }

        public Categoria Delete(int? id)
        {
            if(id == null)
            {
                throw new Exception("ID da categoria não existe.");
            }

            var categoria = _context.Categorias.Find(id);

            if(categoria is null)
            {
                throw new Exception("Categoria não encatrada. ");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return categoria;
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            var categorias = _context.Categorias.ToList();

            if(categorias is null)
            {
                throw new Exception("Categorias não encontradas.");
            }

            return categorias;
        }

        public Categoria GetCategoriaPorId(int? id)
        {
            if(id is null)
            {
                throw new Exception("ID da categoria não existe.");
            }

            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaID == id);

            if(categoria is null)
            {
                throw new Exception("Categoria não encontrada.");
            }

            return categoria; 
        }

        public Categoria Update(Categoria categoria)
        {
            if(categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            _context.Entry(categoria).State = EntityState.Modified;

            _context.SaveChanges();

            return categoria;
        }
    }
}
