using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.Data.Repository.Base;
using MyFridge_Library_Data.Data.Repository.Interface;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public override async Task<bool> UpdateAsync(Order updateEntity)
        {
            if (updateEntity == null) return false;

            Order? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Status = updateEntity.Status;

            return true;
        }

        #region Grocery
        public async Task<bool> AddGroceryAsync(int id, Grocery addEntity)
        {
            Task<Order?> t1 = GetAsync(id);
            Task<Grocery?> t2 = _context.Groceries
                .Where(grocery => grocery.Id == addEntity.Id)
                .FirstOrDefaultAsync();

            await Task.WhenAll(t1, t2);

            Order? orderEntityInDb = t1.Result;
            Grocery? groceryEntityInDb = t2.Result;

            if (orderEntityInDb == null) return false;

            if (groceryEntityInDb == null)
                orderEntityInDb.Groceries.Add(addEntity);
            else
                orderEntityInDb.Groceries.Add(groceryEntityInDb);
            return true;
        }

        public async Task<bool> RemoveGroceryAsync(int id, int index)
        {
            Order? entityInDb = await GetAsync(id);

            if (entityInDb == null) return false;

            Grocery order = entityInDb.Groceries.ElementAt(index);
            entityInDb.Groceries.Remove(order);

            return true;
        }
        #endregion
    }
}
