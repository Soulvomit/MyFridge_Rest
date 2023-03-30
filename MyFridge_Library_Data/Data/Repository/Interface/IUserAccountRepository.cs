using MyFridge_Library_Data.Data.Repository.Interface.Base;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository.Interface
{
    public interface IUserAccountRepository : IRepository<UserAccount>
    {
        public Task<UserAccount> GetByEmailAsync(string email);
        public Task<bool> ChangeAddressAsync(int id, Address changeEntity);
        public Task<bool> AddIngredientAsync(int id, Ingredient addEntity, float addAmount);
        public Task<bool> AddIngredientAsync(int id, IngredientAmount addEntity);
        public Task<bool> RemoveIngredientAsync(int id, int index);
        public Task<bool> RemoveAmountAsync(int id, Ingredient updateEntity, float removeAmount, bool forceRemove = true);
        public Task<bool> AddOrderAsync(int id, Order addEntity);
        public Task<bool> RemoveOrderAsync(int id, int index);
        public Task<List<Recipy>> GetValidRicipiesAsync(int id);
        public Task<bool> MakeRicipiesAsync(int id, Recipy recipy, bool force = false);
    }
}
