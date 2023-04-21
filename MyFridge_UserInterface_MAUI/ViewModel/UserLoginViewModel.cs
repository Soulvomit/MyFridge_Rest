using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.ComponentModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserLoginViewModel : INotifyPropertyChanged
    {
        private readonly UserViewModel _uvm;
        private readonly CurrentUserService _cUserService;
        private string loginResultMsg;
        public string EntryEmail { get; set; }
        public string EntryPassword { get; set; }
        public string LoginResultMsg
        {
            get => loginResultMsg;
            set
            {
                loginResultMsg = value;
                OnPropertyChanged(nameof(LoginResultMsg));
            }
        }
        public UserLoginViewModel(CurrentUserService cUserService, UserViewModel uvm)
        {
            _cUserService = cUserService;
            _uvm = uvm;

            EntryEmail = "email@email.com";
            EntryPassword = "password";
            LoginResultMsg = "Test";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> Login()
        {
            UserAccountDto user = await _cUserService.Client.GetByEmailAsync(EntryEmail);

            if (user.Id != 0)
            {
                if (EntryPassword == user.Password)
                {
                    await _uvm.Initialize(user.Id);

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
