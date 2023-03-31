using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserLogoutPage : ContentPage
{
    public UserLogoutPage()
    {
        InitializeComponent();

        EmailLabel.Text = UserService.Instance.UserVM.UserAccount.Email;
    }

    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        UserService.Instance.UserVM = null;

        await Navigation.PopAsync();
    }
}