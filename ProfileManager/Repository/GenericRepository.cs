using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProfileManager.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace ProfileManager.Repository
{
    public class GenericRepository<T> : /*IDisposable,*/ IGenericRepository<T> where T : class
    {
        protected readonly ProfileManagerDataDbContext _dbContext;
        private DbSet<T> _dbSet = null;

        public GenericRepository(ProfileManagerDataDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<int> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            //await _dbContext.Set<T>().AddAsync(entity);
            var res = _dbContext.SaveChanges();
            return res;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            //_dbContext.Set<T>().AddRange(entities);
        }


        public IQueryable<T> Find(Expression<Func<T, bool>> expression, params Expression<Func<T, Object>>[]? includes)
        {
            if (includes.Length > 0)
            {
                IQueryable<T> query = _dbSet.Where(expression).Include(includes[0]);
                //IQueryable<T> query = _dbContext.Set<T>().Where(expression).Include(includes[0]);
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return query;
            }

            var queryAll = _dbSet.Where(expression);
            //var queryAll = _dbContext.Set<T>().Where(expression);

            return queryAll;
        }


        public virtual IQueryable<T> GetAll(params Expression<Func<T, Object>>[] includes)
        {
            if (includes.Length > 0)
            {
                IQueryable<T> query = _dbSet.Include(includes[0]);
                //IQueryable<T> query = _dbContext.Set<T>().Include(includes[0]);
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return query;
            }

            var queryAll = _dbSet;
            //var queryAll = _dbContext.Set<T>();

            return queryAll;
        }

        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
            //return _dbContext.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public async Task UpdateAsync(T obj, bool exceptNull = true)
        {
            _dbSet.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;

            if (exceptNull)
            {
                var entry = _dbContext.Entry(obj);
                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType.IsEnum)
                    {
                        if ((int)entry.Property(property.Name).CurrentValue == 0)
                        {
                            entry.Property(property.Name).IsModified = false;
                        }
                        
                    }
                    if (property.GetValue(obj, null) == null)
                    {
                        entry.Property(property.Name).IsModified = false;
                    }
                }
            }
         

            //await _dbContext.SaveChangesAsync();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }


        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            _dbContext.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}