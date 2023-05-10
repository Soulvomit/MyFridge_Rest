using Data_Library.Context;
using Data_Library.Repository.Abstract;
using Data_Library.Repository.Interface;
using Data_Model.Model;
using Microsoft.Extensions.Logging;

namespace Data_Library.Repository
{
    public class GroceryDataRepository : DataRepository<GroceryDto>, IGroceryDataRepository
    {
        public GroceryDataRepository(ApplicationDbContext context, ILogger log)
            : base(context, log)
        {
        }
        public override async Task<bool> UpdateAsync(GroceryDto updateEntity)
        {
            if (updateEntity == null) return false;

            GroceryDto? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Brand = updateEntity.Brand;
            entityInDb.SalePrice = updateEntity.SalePrice;
            entityInDb.ItemIdentifier = updateEntity.ItemIdentifier;
            entityInDb.ImageUrl = updateEntity.ImageUrl;

            return true;
        }
    }
}