using Data_Library.Context;
using Data_Library.Repository.Abstract;
using Data_Library.Repository.Interface;
using Data_Model.Model;
using Microsoft.Extensions.Logging;

namespace Data_Library.Repository
{
    public class IngredientAmountDataRepository : DataRepository<IngredientAmountDto>, IIngredientAmountDataRepository

    {
        public IngredientAmountDataRepository(ApplicationDbContext context, ILogger log)
            : base(context, log)
        {
        }

        public override async Task<bool> UpdateAsync(IngredientAmountDto updateEntity)
        {
            if (updateEntity == null) return false;

            IngredientAmountDto? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Amount = updateEntity.Amount;
            entityInDb.ExpirationDate = updateEntity.ExpirationDate;

            return true;
        }
    }
}
