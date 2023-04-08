using MyFridge_Library_MAUI_Client;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.Service
{
    public sealed class IngredientService
    {
        private List<IngredientDto> _ingredients = null;

        public IngredientApiClient IngredientClient { get; private set; }

        public IngredientService()
        {
            string baseAddr = "https://localhost:44364/";
            IngredientClient = new IngredientApiClient(baseAddr);
        }

        public async Task<List<IngredientDto>> GetIngredientsAsync()
        {
            _ingredients = await IngredientClient.GetAllAsync();
            return _ingredients;
        }
        public async Task<List<IngredientDto>> GetIngredientsLazy()
        {
            if (_ingredients == null)
                return await GetIngredientsAsync();

            return _ingredients;
        }
    }
}
