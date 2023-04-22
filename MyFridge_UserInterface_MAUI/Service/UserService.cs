using MyFridge_Library_MAUI_Client;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.Service
{
    public sealed class UserService
    {
        private List<UserAccountDto> _users = null;

        public UserAccountApiClient Client { get; private set; }

        public UserService()
        {
            string baseAddress = "https://localhost:44364/";
            Client = new UserAccountApiClient(baseAddress);
        }

        public async Task<List<UserAccountDto>> GetAllAsync()
        {
            _users = await Client.GetAllAsync();
            return _users;
        }
        public async Task<List<UserAccountDto>> GetAllLazyAsync()
        {
            if (_users == null)
                return await GetAllAsync();

            return _users;
        }
    }
}
