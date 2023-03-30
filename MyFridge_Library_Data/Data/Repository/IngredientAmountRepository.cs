using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.Data.Repository.Base;
using MyFridge_Library_Data.Data.Repository.Interface;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository
{
    public class IngredientAmountRepository : Repository<IngredientAmount>, IIngredientAmountRepository

    {
        public IngredientAmountRepository(ApplicationDbContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public override async Task<bool> UpdateAsync(IngredientAmount updateEntity)
        {
            if (updateEntity == null) return false;

            IngredientAmount? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Amount = updateEntity.Amount;
            entityInDb.ExpirationDate = updateEntity.ExpirationDate;

            return true;
        }

        #region Ingredient
        public async Task<bool> ChangeIngredientAsync(int id, Ingredient changeEntity)
        {
            Task<IngredientAmount?> t1 = GetAsync(id);
            Task<Ingredient?> t2 = _context.Ingredients
                .Where(ingredient => ingredient.Id == changeEntity.Id)
                .FirstOrDefaultAsync();

            await Task.WhenAll(t1, t2);

            IngredientAmount? ingredientAmountEntityInDb = t1.Result;
            Ingredient? ingredientEntityInDb = t2.Result;

            if (ingredientAmountEntityInDb == null) return false;

            if (ingredientEntityInDb == null)
                ingredientAmountEntityInDb.Ingredient = changeEntity;
            else
                ingredientAmountEntityInDb.Ingredient = ingredientEntityInDb;

            return true;
        }
        #endregion
    }
}
