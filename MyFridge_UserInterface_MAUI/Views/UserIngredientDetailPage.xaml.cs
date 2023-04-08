using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserIngredientDetailPage : ContentPage
{
    private UserIngredientDetailViewModel _vm;
    public UserIngredientDetailPage(UserIngredientDetailViewModel vm)
    {
        InitializeComponent();

        _vm = vm;
        BindingContext = _vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        Name.Text = _vm.Ingredient.Name;
        UnitStr.Text = _vm.UnitStr;
    }
    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        _vm.Ingredient.Amount = float.Parse(Amount.Text);
        _vm.Ingredient.ExpirationDate = ExpirationDate.Date;
        await Navigation.PopAsync();
    }

    private void OnAmountCompleted(object sender, EventArgs e)
    {
        Amount.Unfocus();
    }
}