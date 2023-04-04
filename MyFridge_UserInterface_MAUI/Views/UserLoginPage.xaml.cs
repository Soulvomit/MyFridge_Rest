using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.Views
{
    public partial class UserLoginPage : ContentPage
    {
        private readonly UserService _userService;
        public UserLoginPage(UserService userService)
        {
            InitializeComponent();

            _userService = userService;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            EmailEntry.Text = "email@email.com";
            PasswordEntry.Text = "password";
        }
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            UserAccountDto user = await _userService.UserClient.GetUserAccountByEmailAsync(email);

            if (user != null)
            {
                if (password == user.Password)
                {
                    _userService.User = user;

                    await Shell.Current.GoToAsync($"//" + nameof(UserInfoPage));
                }
                else
                    LoginResultLabel.Text = "Invalid email or password";
            }
            else
                LoginResultLabel.Text = "No user with that email exists";
        }
    }
}