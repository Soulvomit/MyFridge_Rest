using MyFridge_Library_Data.DataModel;
using MyFridge_Library_Data.DataRepository.Interface.Base;

namespace MyFridge_Library_Data.DataRepository.Interface
{
    public interface IOrderDataRepository : IDataRepository<Order>
    {
        public Task<bool> AddGroceryAsync(int id, Grocery addEntity);
        public Task<bool> RemoveGroceryAsync(int id, int groceryId);
    }
}
