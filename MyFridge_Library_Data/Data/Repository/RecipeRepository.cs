using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.Data.Repository.Base;
using MyFridge_Library_Data.Data.Repository.Interface;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public override async Task<bool> UpdateAsync(Recipe updateEntity)
        {
            if (updateEntity == null) return false;

            Recipe? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Name = updateEntity.Name;
            entityInDb.Method = updateEntity.Method;
            entityInDb.ImageUrl = updateEntity.ImageUrl;

            return true;
        }

        #region Ingredients
        public async Task<bool> AddIngredientAsync(int id, Ingredient addEntity, float addAmount)
        {
            Task<Recipe?> t1 = GetAsync(id);
            Task<Ingredient?> t2 = _context.Ingredients
                .Where(ingredient => ingredient.Id == addEntity.Id)
                .FirstOrDefaultAsync();

            await Task.WhenAll(t1, t2);

            Recipe? recipeEntityInDb = t1.Result;
            Ingredient? ingredientEntityInDb = t2.Result;

            if (recipeEntityInDb == null) return false;

            if (ingredientEntityInDb == null)
            {
                recipeEntityInDb.IngredientAmounts.Add(
                    new IngredientAmount
                    {
                        Ingredient = addEntity,
                        Amount = addAmount
                    });
            }
            else
            {
                recipeEntityInDb.IngredientAmounts.Add(
                    new IngredientAmount
                    {
                        Ingredient = ingredientEntityInDb,
                        Amount = addAmount
                    });
            }

            return true;
        }
        public async Task<bool> AddIngredientAsync(int id, IngredientAmount addEntity)
        {
            Task<Recipe?> t1 = GetAsync(id);
            Task<IngredientAmount?> t2 = _context.IngredientAmounts
                .Where(ingredient => ingredient.Id == addEntity.Id)
                .FirstOrDefaultAsync();

            await Task.WhenAll(t1, t2);

            Recipe? recipeEntityInDb = t1.Result;
            IngredientAmount? ingredientAmountEntityInDb = t2.Result;

            if (recipeEntityInDb == null) return false;

            if (ingredientAmountEntityInDb == null)
                recipeEntityInDb.IngredientAmounts.Add(addEntity);
            else
                recipeEntityInDb.IngredientAmounts.Add(ingredientAmountEntityInDb);

            return true;
        }
        public async Task<bool> RemoveIngredientAsync(int id, int iaId)
        {
            Recipe? entityInDb = await GetAsync(id);
            if (entityInDb == null) return false;

            IngredientAmount? iaEntityInDb = await _context.IngredientAmounts.FindAsync(iaId);
            if (iaEntityInDb == null) return false;

            entityInDb.IngredientAmounts.Remove(iaEntityInDb);
            _context.IngredientAmounts.Remove(iaEntityInDb);

            return true;
        }
        #endregion
    }
}
