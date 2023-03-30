using MyFridge_Library_Data.Data.Repository.Interface.Base;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository.Interface
{
    public interface IUserAccountRepository : IRepository<UserAccount>
    {
        public Task<UserAccount> GetByEmailAsync(string email);
        public Task<bool> AddIngredientAsync(int id, Ingredient addEntity, float addAmount);
        public Task<bool> AddIngredientAmountAsync(int id, IngredientAmount addEntity);
        public Task<bool> RemoveIngredientAmountAsync(int id, int ingredientId, float removeAmount, bool forceRemove = true);
        public Task<bool> AddOrderAsync(int id, Order addEntity);
        public Task<bool> RemoveOrderAsync(int id, int index);
    }
}
