using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using System.Net.Http.Json;

namespace MyFridge_Library_MAUI_Client
{
    public class UserAccountApiClient
    {
        private readonly HttpClient _httpClient;

        public UserAccountApiClient(string baseAddress)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
        }
        public async Task<UserAccountDto> UpsertAsync(UserAccountDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/UserAccount/Upsert", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserAccountDto>();
        }
        public async Task<UserAccountDto> GetAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/UserAccount/Get?id={id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserAccountDto>();
        }

        public async Task<UserAccountDto> GetByEmailAsync(string email)
        {
            var response = await _httpClient.GetAsync($"api/UserAccount/GetByEmail?email={email}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserAccountDto>();
        }
        public async Task<List<UserAccountDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"api/UserAccount/GetAll");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<UserAccountDto>>();
        }
        public async Task<IngredientAmountDto> AddIngredientAsync(IngredientAmountDto dto, int id)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/UserAccountIngredients/Upsert?id={id}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IngredientAmountDto>();
        }

        public async Task<bool> RemoveIngredientAsync(int id, int ingredientId)
        {
            var response = await _httpClient.DeleteAsync($"api/UserAccountIngredients/Delete?id={id}&index={ingredientId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}