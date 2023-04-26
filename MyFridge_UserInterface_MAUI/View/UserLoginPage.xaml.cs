using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.View
{
    public partial class UserLoginPage : ContentPage
    {
        private readonly UserLoginViewModel _viewModel;
        public UserLoginPage(UserLoginViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            BindingContext = _viewModel;
        }
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            bool success = await _viewModel.LoginAsync();
            if (success)
            {
                await Shell.Current.GoToAsync($"//" + nameof(UserInfoPage));
            }
        }
    }
}