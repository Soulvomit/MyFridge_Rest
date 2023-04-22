using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserLoginViewModel : BindableObject
    {
        private readonly UserViewModel _userViewModel;
        private readonly CurrentUserService _cUserService;
        private string loginResultMsg;
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
        public string LoginResultMsg
        {
            get => loginResultMsg;
            set
            {
                loginResultMsg = value;
                OnPropertyChanged(nameof(LoginResultMsg));
            }
        }
        public UserLoginViewModel(CurrentUserService cUserService, UserViewModel userViewModel)
        {
            _cUserService = cUserService;
            _userViewModel = userViewModel;

            EntryEmail = "email@email.com";
            EntryPassword = "password";
        }

        public async Task<bool> LoginAsync()
        {
            UserAccountDto user = await _cUserService.Client.GetByEmailAsync(EntryEmail);

            if (user.Id != 0)
            {
                if (EntryPassword == user.Password)
                {
                    await _userViewModel.InitializeAsync(user.Id);

                    return true;
                }
                else
                {
                    LoginResultMsg = "Invalid password";
                }
            }
            else
            {
                LoginResultMsg = "No user with that email exists";
            }
            return false;
        }
    }
}
