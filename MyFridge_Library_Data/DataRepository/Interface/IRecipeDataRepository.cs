using MyFridge_Library_Data.DataModel;
using MyFridge_Library_Data.DataRepository.Interface.Base;

namespace MyFridge_Library_Data.DataRepository.Interface
{
    public interface IRecipeDataRepository : IDataRepository<Recipe>

    {
        public Task<bool> BatchIngredientAmountAsync(int id, IngredientAmount addEntity);
        public Task<bool> AddIngredientAmountAsync(int id, IngredientAmount addEntity);
        public Task<bool> RemoveAmountAsync(int id, int ingredientAmountId, float removeAmount, bool forceRemove = true);
        public Task<bool> RemoveIngredientAmountAsync(int id, IngredientAmount removeEntity);
        public Task<bool> RemoveIngredientAmountAsync(int id, int ingredientAmountId);
    }
}