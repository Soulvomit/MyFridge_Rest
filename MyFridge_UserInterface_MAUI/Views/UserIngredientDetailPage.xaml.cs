using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserIngredientDetailPage : ContentPage
{
    private IngredientDto _ingredient;
    public UserIngredientDetailPage(IngredientDto ingredient)
    {
        InitializeComponent();

        _ingredient = ingredient;
        BindingContext = _ingredient;
        Name.Text = _ingredient.Name;
        UnitStr.Text = _ingredient.UnitStr;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        _ingredient.Amount = float.Parse(Amount.Text);
        _ingredient.ExpirationDate = ExpirationDate.Date;
        await Navigation.PopAsync();
    }

    private void OnAmountCompleted(object sender, EventArgs e)
    {
        Amount.Unfocus();
    }
}