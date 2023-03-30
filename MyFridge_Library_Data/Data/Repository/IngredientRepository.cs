using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.Data.Repository.Base;
using MyFridge_Library_Data.Data.Repository.Interface;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(ApplicationDbContext context, ILogger logger)
            : base(context, logger)
        {
        }
        public override async Task<bool> UpdateAsync(Ingredient updateEntity)
        {
            if (updateEntity == null) return false;

            Ingredient? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Name = updateEntity.Name;
            entityInDb.Unit = updateEntity.Unit;

            return true;
        }
    }
}
