using MyFridge_Library_MAUI_Client;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.Service
{
    public sealed class RecipyService
    {
        private List<RecipyDto> _recipies = null;

        public RecipyApiClient Client { get; private set; }

        public RecipyService()
        {
            string baseAddr = "https://localhost:44364/";
            Client = new RecipyApiClient(baseAddr);
        }

        public async Task<List<RecipyDto>> GetAllAsync()
        {
            _recipies = await Client.GetAllAsync();
            return _recipies;
        }
        public async Task<List<RecipyDto>> GetAllLazyAsync()
        {
            if (_recipies == null)
                return await GetAllAsync();

            return _recipies;
        }
    }
}
