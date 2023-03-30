using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.Data.Repository.Base;
using MyFridge_Library_Data.Data.Repository.Interface;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository
{
    public class RecipyRepository : Repository<Recipy>, IRecipyRepository
    {
        public RecipyRepository(ApplicationDbContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public override async Task<bool> UpdateAsync(Recipy updateEntity)
        {
            if (updateEntity == null) return false;

            Recipy? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Name = updateEntity.Name;
            entityInDb.Description = updateEntity.Description;

            return true;
        }

        #region Ingredients
        public async Task<bool> AddIngredientAsync(int id, Ingredient addEntity, float addAmount)
        {
            Task<Recipy?> t1 = GetAsync(id);
            Task<Ingredient?> t2 = _context.Ingredients
                .Where(ingredient => ingredient.Id == addEntity.Id)
                .FirstOrDefaultAsync();

            await Task.WhenAll(t1, t2);

            Recipy? recipyEntityInDb = t1.Result;
            Ingredient? ingredientEntityInDb = t2.Result;

            if (recipyEntityInDb == null) return false;

            if (ingredientEntityInDb == null)
            {
                recipyEntityInDb.IngredientAmounts.Add(
                    new IngredientAmount
                    {
                        Ingredient = addEntity,
                        Amount = addAmount
                    });
            }
            else
            {
                recipyEntityInDb.IngredientAmounts.Add(
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
            Task<Recipy?> t1 = GetAsync(id);
            Task<IngredientAmount?> t2 = _context.IngredientAmounts
                .Where(ingredient => ingredient.Id == addEntity.Id)
                .FirstOrDefaultAsync();

            await Task.WhenAll(t1, t2);

            Recipy? recipyEntityInDb = t1.Result;
            IngredientAmount? ingredientAmountEntityInDb = t2.Result;

            if (recipyEntityInDb == null) return false;

            if (ingredientAmountEntityInDb == null)
                recipyEntityInDb.IngredientAmounts.Add(addEntity);
            else
                recipyEntityInDb.IngredientAmounts.Add(ingredientAmountEntityInDb);

            return true;
        }
        public async Task<bool> RemoveIngredientAsync(int id, int iaId)
        {
            Recipy? entityInDb = await GetAsync(id);
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
