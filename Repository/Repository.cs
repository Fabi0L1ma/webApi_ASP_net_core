using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Context;

namespace WebApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public T Create(T entidade)
        {
            _context.Set<T>().Add(entidade);
            //_context.SaveChanges();

            return entidade;
        }

        public T Delete(T entidade)
        {
            _context.Set<T>().Remove(entidade);
            //_context.SaveChanges();

            return entidade;
        }

        public T? Get(Expression<Func<T, bool>> filtro)
        {
            return _context.Set<T>().FirstOrDefault(filtro);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T Update(T entidade)
        {
            _context.Set<T>().Update(entidade);
            //_context.SaveChanges();
            
            return entidade;
        }
    }
}
