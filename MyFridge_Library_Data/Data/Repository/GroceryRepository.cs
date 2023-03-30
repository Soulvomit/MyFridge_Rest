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
            entityInDb.SalePriceDKK = updateEntity.SalePriceDKK;

            return true;
        }

        #region Ingredient
        public async Task<bool> ChangeIngredientAsync(int id, Ingredient changeEntity, float changeAmount)
        {
            Task<Grocery?> t1 = GetAsync(id);
            Task<Ingredient?> t2 = _context.Ingredients
                .Where(ingredient => ingredient.Id == changeEntity.Id)
                .FirstOrDefaultAsync();

            await Task.WhenAll(t1, t2);

            Grocery? groceryEntityInDb = t1.Result;
            Ingredient? ingredientEntityInDb = t2.Result;

            if (groceryEntityInDb == null) return false;

            if (ingredientEntityInDb == null)
                groceryEntityInDb.IngredientAmount!.Ingredient = changeEntity;
            else
                groceryEntityInDb.IngredientAmount!.Ingredient = ingredientEntityInDb;

            groceryEntityInDb.IngredientAmount!.Amount = changeAmount;

            return true;
        }

        public async Task<bool> ChangeIngredientAsync(int id, IngredientAmount changeEntity)
        {
            Task<Grocery?> t1 = GetAsync(id);
            Task<IngredientAmount?> t2 = _context.IngredientAmounts
                .Where(ingredient => ingredient.Id == changeEntity.Id)
                .FirstOrDefaultAsync();

            await Task.WhenAll(t1, t2);

            Grocery? groceryEntityInDb = t1.Result;
            IngredientAmount? ingredientAmountEntityInDb = t2.Result;

            if (groceryEntityInDb == null) return false;

            if (ingredientAmountEntityInDb == null)
                groceryEntityInDb.IngredientAmount = changeEntity;
            else
                groceryEntityInDb.IngredientAmount = ingredientAmountEntityInDb;

            return true;
        }
        #endregion
    }
}
