using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class IngredientPage : ContentPage
{
    private readonly IngredientViewModel _vm;
    public IngredientPage(IngredientViewModel vm)
    {
        InitializeComponent();

        _vm = vm;
        BindingContext = _vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _vm.GetIngredientDetailsAsync();
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            _vm.GetIngredientDetailsLazyAsync();
        else
            _vm.GetIngredientDetailsFilteredLazyAsync(e.NewTextValue);
    }
    private async void OnIngredientSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is UserIngredientDetailViewModel selectedIngredient)
        {
            bool parsed = uint.TryParse(
                await DisplayPromptAsync(
                    "Add Amount",
                    "Enter the amount",
                    "OK",
                    "Cancel",
                    "0",
                    -1,
                    Keyboard.Numeric,
                    ""),
                out uint amount);
            if (parsed)
            {
                selectedIngredient.Ingredient.Amount = amount;
                await _vm._cUserService.UserClient
                    .AddIngredientAsync(selectedIngredient.Ingredient, _vm._cUserService.CurrentUserId);
                await Navigation.PopAsync();
            }
        }
        //deselect the item
        (sender as CollectionView).SelectedItem = null;
    }
}