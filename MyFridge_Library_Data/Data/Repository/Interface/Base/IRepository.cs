namespace MyFridge_Library_Data.Data.Repository.Interface.Base
{
    public interface IRepository<T> where T : class
    {
        public Task<bool> CreateAsync(T entity);
        public Task<bool> UpdateAsync(T updateEntity);
        public Task<bool> DeleteAsync(int id);
        public Task<T?> GetAsync(int id);
        public Task<List<T>> GetAllAsync();
    }
}
