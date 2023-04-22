using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using System.Net.Http.Json;

namespace MyFridge_Library_MAUI_Client
{
    public class IngredientAmountApiClient
    {
        private readonly HttpClient _httpClient;

        public IngredientAmountApiClient(string baseAddress)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
        }
        public async Task<IngredientAmountDto> UpsertAsync(IngredientAmountDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/IngredientAmount/Upsert", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IngredientAmountDto>();
        }
        public async Task<IngredientAmountDto> GetAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/IngredientAmount/Get?id={id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IngredientAmountDto>();
        }
    }
}
