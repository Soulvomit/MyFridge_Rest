using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
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
    }
}
