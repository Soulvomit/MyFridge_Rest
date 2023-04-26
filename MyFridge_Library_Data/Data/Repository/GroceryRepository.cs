using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.Data.Repository.Base;
using MyFridge_Library_Data.Data.Repository.Interface;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository
{
    public class GroceryRepository : Repository<Grocery>, IGroceryRepository 
    {
        public GroceryRepository(ApplicationDbContext context, ILogger logger)
            : base(context, logger)
        {
        }
        public override async Task<bool> UpdateAsync(Grocery updateEntity)
        {
            if (updateEntity == null) return false;

            Grocery? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Brand = updateEntity.Brand;
            entityInDb.Category = updateEntity.Category;
            entityInDb.SalePrice = updateEntity.SalePrice;
            entityInDb.ItemIdentifier = updateEntity.ItemIdentifier;
            entityInDb.ImageUrl = updateEntity.ImageUrl;

            return true;
        }
    }
}