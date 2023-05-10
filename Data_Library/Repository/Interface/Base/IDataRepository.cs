namespace Data_Library.Repository.Interface.Base
{
    public interface IDataRepository<T> where T : class
    {
        public Task<bool> CreateAsync(T entity);
        public Task<bool> UpdateAsync(T updateEntity);
        public Task<bool> DeleteAsync(int id);
        public Task<T?> GetAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task<List<T>?> Query(Func<T, bool> filterFunc, Func<T, object> orderByFunc, string filter, int minLength = 2);
        public Task<List<T>?> Query(Func<T, bool> filterFunc, Func<T, object> orderByFunc);
    }
}
