using MyFridge_Library_Data.Data.Repository.Interface.Base;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository.Interface
{
    public interface IRecipeRepository : IRepository<Recipe>
   
    {
        public Task<bool> BatchIngredientAmountAsync(int id, IngredientAmount addEntity);
        public Task<bool> AddIngredientAmountAsync(int id, IngredientAmount addEntity);
        public Task<bool> RemoveAmountAsync(int id, int ingredientAmountId, float removeAmount, bool forceRemove = true);
        public Task<bool> RemoveIngredientAmountAsync(int id, IngredientAmount removeEntity);
        public Task<bool> RemoveIngredientAmountAsync(int id, int ingredientAmountId);
    }
}