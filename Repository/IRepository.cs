using System.Linq.Expressions;

namespace WebApi.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? Get(Expression<Func<T, bool>> filtro);
        T Create(T entidade);
        T Update(T entidade);
        T Delete(T entidade);
    }
}
