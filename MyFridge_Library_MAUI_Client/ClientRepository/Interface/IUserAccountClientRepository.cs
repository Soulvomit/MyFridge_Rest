using MyFridge_Library_MAUI_Client.ClientRepository.Interface.Base;
using MyFridge_Library_MAUI_Client.ClientModel;

namespace MyFridge_Library_MAUI_Client.ClientRepository.Interface
{
    public interface IUserAccountClientRepository : IClientRepository<UserAccountDto>
    {
        public Task<UserAccountDto> GetByEmailAsync(string email);
        public Task<IngredientAmountDto> AddIngredientAmountAsync(IngredientAmountDto dto, int id);
        public Task<bool> RemoveIngredientAmountAsync(int id, int ingredientId, int removeAmount, bool force = false);
    }
}
