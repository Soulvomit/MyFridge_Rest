using MyFridge_UserInterface_MAUI.Service;
using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserLogoutPage : ContentPage
{
    private UserViewModel _vm;
    public UserLogoutPage(UserViewModel vm)
    {
        InitializeComponent();

        _vm = vm;
        BindingContext = _vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        //UserService.Instance.UserVM = null;

        await Navigation.PopAsync();
    }
}