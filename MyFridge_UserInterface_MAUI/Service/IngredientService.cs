using MyFridge_Library_MAUI_Client;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.Service
{
    public sealed class IngredientService
    {
        private List<IngredientDto> _ingredients = null;

        public IngredientApiClient Client { get; private set; }

        public IngredientService()
        {
            string baseAddr = "https://localhost:44364/";
            Client = new IngredientApiClient(baseAddr);
        }

        public async Task<List<IngredientDto>> GetAllAsync()
        {
            _ingredients = await Client.GetAllAsync();
            return _ingredients;
        }
        public async Task<List<IngredientDto>> GetAllLazyAsync()
        {
            if (_ingredients == null)
                return await GetAllAsync();

            return _ingredients;
        }
    }
}
