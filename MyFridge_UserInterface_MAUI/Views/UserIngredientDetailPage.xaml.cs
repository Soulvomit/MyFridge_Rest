using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserIngredientDetailPage : ContentPage
{
    private readonly IngredientAmountDetailViewModel _vm;
    public UserIngredientDetailPage(IngredientAmountDetailViewModel vm)
    {
        InitializeComponent();

        _vm = vm;
        BindingContext = _vm;
    }
    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        await _vm.SaveAsync();
        await Navigation.PopAsync();
    }

    private void OnAmountCompleted(object sender, EventArgs e)
    {
        (sender as Entry).Unfocus();
    }
}