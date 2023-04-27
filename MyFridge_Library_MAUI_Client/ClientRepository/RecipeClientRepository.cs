using MyFridge_Library_MAUI_Client.ClientRepository.Abstract;
using MyFridge_Library_MAUI_Client.ClientRepository.Interface;
using MyFridge_Library_MAUI_Client.ClientModel;
using System.Net.Http.Json;

namespace MyFridge_Library_MAUI_Client.ClientRepository
{
    public class RecipeClientRepository : ClientRepository<RecipeDto>, IRecipeClientRepository
    {
        public RecipeClientRepository(string baseAddress) : base(baseAddress) { }

        public IEnumerable<RecipeDto> MakeableLazies { get; private set; }

        public async Task<IEnumerable<RecipeDto>> GetMakeableAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"api/{ResolveName}/GetMakeable?userId={userId}");
            response.EnsureSuccessStatusCode();
            MakeableLazies = await response.Content.ReadFromJsonAsync<IEnumerable<RecipeDto>>();
            return MakeableLazies;
        }
    }
}
