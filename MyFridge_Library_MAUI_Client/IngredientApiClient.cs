using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using System.Net.Http.Json;

namespace MyFridge_Library_MAUI_Client
{
    public class IngredientApiClient
    {
        private readonly HttpClient _httpClient;

        public IngredientApiClient(string baseAddress)
        {
            _httpClient = new();
            _httpClient.BaseAddress = new(baseAddress);
        }
        public async Task<List<IngredientDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"api/Ingredient/GetAll");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<IngredientDto>>();
        }
    }
}
