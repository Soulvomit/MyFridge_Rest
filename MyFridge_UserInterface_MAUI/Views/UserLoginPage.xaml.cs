using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views
{
    public partial class UserLoginPage : ContentPage
    {
        private readonly UserLoginViewModel _vm;
        public UserLoginPage(UserLoginViewModel vm)
        {
            InitializeComponent();

            _vm = vm;
            BindingContext = _vm;
        }
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            bool success = await _vm.LoginAsync();
            if (success)
            {
                await Shell.Current.GoToAsync($"//" + nameof(UserInfoPage));
            }
        }
    }
}