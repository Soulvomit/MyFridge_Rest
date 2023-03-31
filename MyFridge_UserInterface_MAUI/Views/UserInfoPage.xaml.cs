using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserInfoPage : ContentPage
{
    public UserInfoPage()
    {
        InitializeComponent();

        //set the text of each label to display the user's information
        FirstNameEntry.Text = UserService.Instance.UserVM.UserAccount.Firstname;
        LastNameEntry.Text = UserService.Instance.UserVM.UserAccount.Lastname;
        EmailEntry.Text = UserService.Instance.UserVM.UserAccount.Email;
        PasswordEntry.Text = UserService.Instance.UserVM.UserAccount.Password;
        PhonenumberEntry.Text = UserService.Instance.UserVM.UserAccount.PhoneNumber.ToString();
        BirthdatePicker.Date = UserService.Instance.UserVM.UserAccount.BirthDate.Date;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        string newFirstName = UserService.Instance.UserVM.UserAccount.Firstname;
        if (!string.IsNullOrEmpty(newFirstName))
        {
            UserService.Instance.UserVM.UserAccount.Firstname = newFirstName;
            FirstNameEntry.Text = newFirstName;
        }
        string newLastName = UserService.Instance.UserVM.UserAccount.Lastname;
        if (!string.IsNullOrEmpty(newLastName))
        {
            UserService.Instance.UserVM.UserAccount.Lastname = newLastName;
            LastNameEntry.Text = newLastName;
        }
        string newEmail = UserService.Instance.UserVM.UserAccount.Email;
        if (!string.IsNullOrEmpty(newEmail))
        {
            UserService.Instance.UserVM.UserAccount.Email = newEmail;
            EmailEntry.Text = newEmail;
        }
        string newPassword = UserService.Instance.UserVM.UserAccount.Password;
        if (!string.IsNullOrEmpty(newPassword))
        {
            UserService.Instance.UserVM.UserAccount.Password = newPassword;
            PasswordEntry.Text = newPassword;
        }
        string newPhonenum = UserService.Instance.UserVM.UserAccount.PhoneNumber.ToString();
        if (ulong.TryParse(newPhonenum, out ulong phonenum))
        {
            UserService.Instance.UserVM.UserAccount.PhoneNumber = phonenum;
            PhonenumberEntry.Text = phonenum.ToString();
        }
        string newBirthdate = UserService.Instance.UserVM.UserAccount.BirthDate.ToShortDateString();
        if (DateTime.TryParse(newBirthdate, out DateTime birthdate))
        {
            UserService.Instance.UserVM.UserAccount.BirthDate = birthdate;
            BirthdatePicker.Date = birthdate.Date;
        }
        await UserService.Instance.UserClient.UpsertUserAccountAsync(UserService.Instance.UserVM.UserAccount);
    }
}