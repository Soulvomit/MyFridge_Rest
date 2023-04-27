namespace MyFridge_Library_MAUI_Client.ClientRepository.Interface.Base
{
    public interface IClientRepository<T> where T : class
    {
        public T Lazy { get; }
        public IEnumerable<T> AllLazies { get; }
        public IEnumerable<T> FilteredLazies { get; }
        public Task<T> UpsertAsync(T dto);
        public Task<T> GetAsync(int id);
        public Task<IEnumerable<T>> GetFilteredAsync(string filter);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> DeleteAsync(int id);
    }
}
