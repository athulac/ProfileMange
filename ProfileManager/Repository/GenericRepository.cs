using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProfileManager.Data;
using System.Linq.Expressions;

namespace ProfileManager.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ProfileManagerDataDbContext _context;
        private DbSet<T> table = null;

        public GenericRepository(ProfileManagerDataDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task<int> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            var res = _context.SaveChanges();
            return res;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }


        public IQueryable<T> Find(Expression<Func<T, bool>> expression, params Expression<Func<T, Object>>[]? includes)
        {
            if (includes.Length > 0)
            {
                IQueryable<T> query = _context.Set<T>().Where(expression).Include(includes[0]);
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return query;
            }

            var queryAll = _context.Set<T>().Where(expression);

            return queryAll;
        }


        public virtual IQueryable<T> GetAll(params Expression<Func<T, Object>>[] includes)
        {
            if (includes.Length > 0)
            {
                IQueryable<T> query = _context.Set<T>().Include(includes[0]);
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return query;
            }

            var queryAll = _context.Set<T>();

            return queryAll;
        }

        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;            
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}