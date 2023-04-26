using MyFridge_Library_MAUI_Client.Interface.Base;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_Library_MAUI_Client.Interface
{
    public interface IUserAccountApiClient : IApiClient<UserAccountDto>
    {
        public Task<UserAccountDto> GetByEmailAsync(string email);
        public Task<IngredientAmountDto> AddIngredientAmountAsync(IngredientAmountDto dto, int id);
        public Task<bool> RemoveIngredientAmountAsync(int id, int ingredientId, int removeAmount, bool force = false);
    }
}
