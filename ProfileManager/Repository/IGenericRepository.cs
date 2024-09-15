using System.Linq.Expressions;

namespace ProfileManager.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(Guid id);
        IQueryable<T> GetAll(params Expression<Func<T, Object>>[] includes);
        IQueryable<T> Find(Expression<Func<T, bool>> expression, params Expression<Func<T, Object>>[] includes);
        Task<int> AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T obj);
        void Save();
    }
}