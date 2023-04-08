using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserInfoPage : ContentPage
{
    private readonly UserViewModel _vm;
    public UserInfoPage(UserViewModel vm)
    {
        InitializeComponent();

        _vm = vm;
        BindingContext = _vm;
    }
    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        await _vm.Commit();
    }
}