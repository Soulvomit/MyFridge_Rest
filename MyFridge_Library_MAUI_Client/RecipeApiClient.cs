using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using System.Net.Http.Json;

namespace MyFridge_Library_MAUI_Client
{
    public class RecipeApiClient
    {
        private readonly HttpClient _httpClient;

        public RecipeApiClient(string baseAddress)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
        }
        public async Task<List<RecipeDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"api/Recipe/GetAll");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<RecipeDto>>();
        }
    }
}
