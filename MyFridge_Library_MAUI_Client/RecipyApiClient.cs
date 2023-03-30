using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using System.Net.Http.Json;

namespace MyFridge_Library_MAUI_Client
{
    public class RecipyApiClient
    {
        private readonly HttpClient _httpClient;

        public RecipyApiClient(string baseAddress)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
        }
        public async Task<List<RecipyDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"api/Recipy/GetAll");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<RecipyDto>>();
        }
    }
}
