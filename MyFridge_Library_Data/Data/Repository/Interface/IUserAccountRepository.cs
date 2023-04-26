using MyFridge_Library_Data.Data.Repository.Interface.Base;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository.Interface
{
    public interface IUserAccountRepository : IRepository<UserAccount>
    {
        public Task<UserAccount?> GetByEmailAsync(string email);
        public Task<bool> BatchIngredientAmountAsync(int id, IngredientAmount addEntity);
        public Task<bool> AddIngredientAmountAsync(int id, IngredientAmount addEntity);
        public Task<bool> RemoveAmountAsync(int id, int ingredientAmountId, float removeAmount, bool forceRemove = true);
        public Task<bool> RemoveIngredientAmountAsync(int id, IngredientAmount removeEntity);
        public Task<bool> RemoveIngredientAmountAsync(int id, int ingredientAmountId);
        public Task<bool> AddOrderAsync(int id, Order addEntity);
        public Task<bool> RemoveOrderAsync(int id, Order removeEntity);
    }
}
