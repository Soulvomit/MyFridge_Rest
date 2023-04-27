using MyFridge_Library_Client_MAUI.ClientRepository.Interface.Base;
using MyFridge_Library_Client_MAUI.ClientModel;

namespace MyFridge_Library_Client_MAUI.ClientRepository.Interface
{
    public interface IUserAccountClientRepository : IClientRepository<UserAccountDto>
    {
        public Task<UserAccountDto> GetByEmailAsync(string email);
        public Task<IngredientAmountDto> AddIngredientAmountAsync(IngredientAmountDto dto, int id);
        public Task<bool> RemoveIngredientAmountAsync(int id, int ingredientId, int removeAmount, bool force = false);
    }
}
