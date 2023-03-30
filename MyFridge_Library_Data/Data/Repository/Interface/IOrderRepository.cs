using MyFridge_Library_Data.Data.Repository.Interface.Base;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository.Interface
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<bool> AddGroceryAsync(int id, Grocery addEntity);
        public Task<bool> RemoveGroceryAsync(int id, int index);
    }
}
