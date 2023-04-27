using MyFridge_Library_MAUI_Client.ClientModel;
using MyFridge_UserInterface_MAUI.Mvvms.Service.Client.Interface;

namespace MyFridge_UserInterface_MAUI.Mvvms.ViewModel
{
    public class UserLoginViewModel : BindableObject
    {
        private readonly IClientService _clientService;
        private string loginResultMessage;
        private string entryEmail;
        private string entryPassword;
        public string EntryEmail
        {
            get => entryEmail;
            set
            {
                entryEmail = value;
                OnPropertyChanged(nameof(EntryEmail));
            }
        }
        public string EntryPassword
        {
            get => entryPassword;
            set
            {
                entryPassword = value;
                OnPropertyChanged(nameof(EntryPassword));
            }
        }
        public string LoginResultMessage
        {
            get => loginResultMessage;
            set
            {
                loginResultMessage = value;
                OnPropertyChanged(nameof(LoginResultMessage));
            }
        }
        public UserLoginViewModel(IClientService clientService)
        {
            _clientService = clientService;

            EntryEmail = "email@email.com";
            EntryPassword = "password";
        }

        public async Task<bool> LoginAsync()
        {
            UserAccountDto user = await _clientService.UserClient.GetByEmailAsync(EntryEmail);

            if (user.Id != 0)
            {
                if (EntryPassword == user.Password)
                {
                    return true;
                }
                else
                {
                    LoginResultMessage = "Invalid password";
                }
            }
            else
            {
                LoginResultMessage = "No user with that email exists";
            }
            return false;
        }
    }
}
