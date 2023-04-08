using MyFridge_Library_MAUI_Client;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.Service
{
    public sealed class RecipyService
    {
        private List<RecipyDto> _recipies = null;

        public RecipyApiClient RecipyClient { get; private set; }

        public RecipyService()
        {
            string baseAddr = "https://localhost:44364/";
            RecipyClient = new RecipyApiClient(baseAddr);
        }

        public async Task<List<RecipyDto>> GetRecipiesAsync()
        {
            _recipies = await RecipyClient.GetAllAsync();
            return _recipies;
        }
        public async Task<List<RecipyDto>> GetRecipiesLazy()
        {
            if (_recipies == null)
                return await GetRecipiesAsync();

            return _recipies;
        }
    }
}
