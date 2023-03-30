using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserLoginPage : ContentPage
{
    public UserLoginPage()
    {
        InitializeComponent();
        EmailEntry.Text = "email@email.com";
        PasswordEntry.Text = "password";
    }
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        
        UserAccountDto user = await UserService.Instance.UserClient.GetUserAccountByEmailAsync(email);

        if (user != null)
        {
            if (password == user.Password)
            {
                UserService.Instance.UserVM = new()
                {
                    UserAccount = user
                };
                await Shell.Current.GoToAsync(nameof(UserIngredientPage));
            }
            else
                LoginResultLabel.Text = "Invalid email or password";
        }
        else 
            LoginResultLabel.Text = "No user with that email exists";        
    }
}