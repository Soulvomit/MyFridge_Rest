using MyFridge_Library_MAUI_Client;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.Service
{
    public sealed class UserService
    {
        private List<UserAccountDto> _users = null;

        public UserAccountApiClient UserClient { get; private set; }

        public UserService()
        {
            string baseAddr = "https://localhost:44364/";
            UserClient = new UserAccountApiClient(baseAddr);
        }

        public async Task<List<UserAccountDto>> GetUsersAsync()
        {
            _users = await UserClient.GetAllAsync();
            return _users;
        }
        public async Task<List<UserAccountDto>> GetUsersLazyAsync()
        {
            if (_users == null)
                return await GetUsersAsync();

            return _users;
        }
    }
}
