using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.DataContext;
using MyFridge_Library_Data.DataModel;
using MyFridge_Library_Data.DataRepository.Abstract;
using MyFridge_Library_Data.DataRepository.Interface;

namespace MyFridge_Library_Data.DataRepository
{
    public class OrderDataRepository : DataRepository<Order>, IOrderDataRepository
    {
        public OrderDataRepository(ApplicationDbContext context, ILogger log)
            : base(context, log)
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
            if (addEntity == null) return false;

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

        public async Task<bool> RemoveGroceryAsync(int id, int groceryId)
        {
            Order? entityInDb = await GetAsync(id);
            if (entityInDb == null) return false;

            Grocery? groceryEntityInDb = await _context.Groceries.FindAsync(groceryId);
            if (groceryEntityInDb == null) return false;

            entityInDb.Groceries.Remove(groceryEntityInDb);

            return true;
        }
        #endregion
    }
}
