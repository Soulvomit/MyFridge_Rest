using Data_Library.Context;
using Data_Library.Repository.Abstract;
using Data_Library.Repository.Interface;
using Data_Model.Model;
using Microsoft.Extensions.Logging;

namespace Data_Library.Repository
{
    public class IngredientDataRepository : DataRepository<IngredientDto>, IIngredientDataRepository
    {
        public IngredientDataRepository(ApplicationDbContext context, ILogger log)
            : base(context, log)
        {
        }
        public override async Task<bool> UpdateAsync(IngredientDto updateEntity)
        {
            if (updateEntity == null) return false;

            IngredientDto? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Name = updateEntity.Name;
            entityInDb.Unit = updateEntity.Unit;
            entityInDb.Category = updateEntity.Category;

            return true;
        }
    }
}
