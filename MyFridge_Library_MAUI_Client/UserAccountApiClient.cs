using MyFridge_Library_MAUI_Client.Base;
using MyFridge_Library_MAUI_Client.Interface;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using System.Net.Http.Json;

namespace MyFridge_Library_MAUI_Client
{
    public class UserAccountApiClient : ApiClient<UserAccountDto>, IUserAccountApiClient
    {
        public UserAccountApiClient(string baseAddress) : base(baseAddress) { }

        public async Task<UserAccountDto> GetByEmailAsync(string email)
        {
            var response = await _httpClient.GetAsync($"api/{ResolveName}/GetByEmail?email={email}");
            response.EnsureSuccessStatusCode();
            Lazy = await response.Content.ReadFromJsonAsync<UserAccountDto>();
            return Lazy;
        }
        public async Task<IngredientAmountDto> AddIngredientAmountAsync(IngredientAmountDto dto, int id)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/{ResolveName}Ingredients/Upsert?id={id}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IngredientAmountDto>();
        }
        public async Task<bool> RemoveIngredientAmountAsync(int id, int ingredientId, int removeAmount, bool force = false)
        {
            var response = await _httpClient.DeleteAsync($"api/{ResolveName}Ingredients/Delete?id={id}&ingredientAmountId={ingredientId}&removeAmount={removeAmount}&forceRemove={force}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}