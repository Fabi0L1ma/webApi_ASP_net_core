using System.Linq.Expressions;

namespace WebApi.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(Expression<Func<T, bool>> filtro);
        T Create(T entidade);
        T Update(T entidade);
        T Delete(T entidade);
    }
}
