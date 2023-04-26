namespace MyFridge_UserInterface_MAUI.View;

public partial class UserLogoutPage : ContentPage
{
    public UserLogoutPage()
    {
        InitializeComponent();
    }

    private async void OnLoaded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//" + nameof(UserLoginPage));
    }
}