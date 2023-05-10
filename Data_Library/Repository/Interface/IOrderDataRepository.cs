using Data_Library.Repository.Interface.Base;
using Data_Model.Model;

namespace Data_Library.Repository.Interface
{
    public interface IOrderDataRepository : IDataRepository<OrderDto>
    {
        public Task<bool> AddGroceryAsync(int id, GroceryDto addEntity);
        public Task<bool> RemoveGroceryAsync(int id, int groceryId);
    }
}
