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
        public async Task<UserAccountDto> UpsertUserAccountAsync(UserAccountDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/UserAccount/Upsert", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserAccountDto>();
        }
        public async Task<UserAccountDto> GetUserAccountAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/UserAccount/GetUserAccount?id={id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserAccountDto>();
        }

        public async Task<UserAccountDto> GetUserAccountByEmailAsync(string email)
        {
            var response = await _httpClient.GetAsync($"api/UserAccount/GetUserAccountByEmail?email={email}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserAccountDto>();
        }

        public async Task<IngredientDto> AddIngredientAsync(IngredientDto dto, int id)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/UserAccount/AddIngredient?id={id}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IngredientDto>();
        }

        public async Task<bool> RemoveIngredientAsync(int id, int index)
        {
            var response = await _httpClient.DeleteAsync($"api/UserAccount/RemoveIngredient?id={id}&index={index}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        //and so on for the other API endpoints...
    }
}