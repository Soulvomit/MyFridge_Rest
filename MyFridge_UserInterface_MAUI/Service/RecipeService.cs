using MyFridge_Library_MAUI_Client;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.Service
{
    public sealed class RecipeService
    {
        private List<RecipeDto> _recipies = null;

        public RecipeApiClient Client { get; private set; }

        public RecipeService()
        {
            string baseAddr = "https://localhost:44364/";
            Client = new RecipeApiClient(baseAddr);
        }

        public async Task<List<RecipeDto>> GetAllAsync()
        {
            _recipies = await Client.GetAllAsync();
            return _recipies;
        }
        public async Task<List<RecipeDto>> GetAllLazyAsync()
        {
            if (_recipies == null)
                return await GetAllAsync();

            return _recipies;
        }
    }
}
