using MyFridge_Library_MAUI_Client;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.Service
{
    public sealed class CurrentUserService
    {
        private UserAccountDto _user = null;
        public int CurrentUserId { get; set; }
        public UserAccountApiClient Client { get; private set; }

        public CurrentUserService()
        {
            string baseAddr = "https://localhost:44364/";
            Client = new UserAccountApiClient(baseAddr);
        }

        public async Task<UserAccountDto> GetUserAsync()
        {
            _user = await Client.GetAsync(CurrentUserId);
            return _user;
        }
        public async Task<UserAccountDto> GetUserLazyAsync()
        {
            if (_user == null)
                return await GetUserAsync();

            return _user;
        }
    }
}
