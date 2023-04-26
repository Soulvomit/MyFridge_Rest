using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.Data.Repository.Interface.Base;

namespace MyFridge_Library_Data.Data.Repository.Base
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;
        protected Repository(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            dbSet = _context.Set<T>();
        } 
        public virtual async Task<bool> CreateAsync(T entity)
        {
            if (entity == null) return false;

            await dbSet.AddAsync(entity);

            return true;
        }

        public abstract Task<bool> UpdateAsync(T updateEntity);

        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? entityInDb = await dbSet.FindAsync(id);

            if (entityInDb == null) return false;

            dbSet.Remove(entityInDb);

            return true;
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            T? entityInDb = await dbSet.FindAsync(id);

            if (entityInDb == null) return null;

            return entityInDb;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<List<T>?> Query(Func<T, bool> filterFunc, Func<T, object> orderByFunc, string filter, int minLength = 2)
        {
            if (filter.Length < minLength)
            {
                return null;
            }

            //get all the entities from the DbSet
            List<T> allEntities = await dbSet.ToListAsync();

            //filter the entities using the provided filter function
            List<T> filteredEntities = allEntities.Where(filterFunc).ToList();

            //sort the filtered entities using the provided order by function if provided
            //if (orderByFunc != null)
            List<T> sortedEntities = filteredEntities.OrderBy(orderByFunc).ToList();

            return sortedEntities;
        }
    }
}
