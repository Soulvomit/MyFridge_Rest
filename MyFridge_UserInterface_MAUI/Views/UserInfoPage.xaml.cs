using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserInfoPage : ContentPage
{
    private readonly UserService _userService;
    public UserInfoPage(UserService userService)
    {
        InitializeComponent();

        _userService = userService;

        //set the text of each label to display the user's information
        FirstNameEntry.Text = _userService.User.Firstname;
        LastNameEntry.Text = _userService.User.Lastname;
        EmailEntry.Text = _userService.User.Email;
        PasswordEntry.Text = _userService.User.Password;
        PhonenumberEntry.Text = _userService.User.PhoneNumber.ToString();
        BirthdatePicker.Date = _userService.User.BirthDate.Date;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        string newFirstName = FirstNameEntry.Text;
        if (!string.IsNullOrEmpty(newFirstName))
        {
            _userService.User.Firstname = newFirstName;
            FirstNameEntry.Text = newFirstName;
        }
        string newLastName = LastNameEntry.Text;
        if (!string.IsNullOrEmpty(newLastName))
        {
            _userService.User.Lastname = newLastName;
            LastNameEntry.Text = newLastName;
        }
        string newEmail = EmailEntry.Text;
        if (!string.IsNullOrEmpty(newEmail))
        {
            _userService.User.Email = newEmail;
            EmailEntry.Text = newEmail;
        }
        string newPassword = PasswordEntry.Text;
        if (!string.IsNullOrEmpty(newPassword))
        {
            _userService.User.Password = newPassword;
            PasswordEntry.Text = newPassword;
        }
        string newPhonenum = PasswordEntry.Text;
        if (ulong.TryParse(newPhonenum, out ulong phonenum))
        {
            _userService.User.PhoneNumber = phonenum;
            PasswordEntry.Text = phonenum.ToString();
        }
        _userService.User.BirthDate = BirthdatePicker.Date;
 
        await _userService.UserClient.UpsertUserAccountAsync(_userService.User);
    }
}