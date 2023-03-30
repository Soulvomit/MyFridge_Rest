using MyFridge_UserInterface_MAUI.Service;
using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class IngredientPage : ContentPage
{
    private List<IngredientViewModel> ingredients;
    public IngredientPage()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();


        ingredients = IngredientViewModel
            .ConvertIngredientDtos(await UserService.Instance.IngredientClient.GetAllAsync());
        IngredientView.ItemsSource = ingredients.OrderBy(i => i.Ingredient.Name);
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            IngredientView.ItemsSource = ingredients.OrderBy(i => i.Ingredient.Name);
        else
            IngredientView.ItemsSource = ingredients
                .Where(i => i.Ingredient.Name.ToLower()
                                  .StartsWith(e.NewTextValue
                                  .ToLower()))
                .OrderBy(i => i.Ingredient.Name);
    }
    private async void OnIngredientTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is IngredientViewModel selectedIngredient)
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
                    "1"), 
                out uint amount);
            if (parsed)
            {
                selectedIngredient.Ingredient.Amount = amount;
                await UserService.Instance.UserClient
                    .AddIngredientAsync(selectedIngredient.Ingredient, 
                        UserService.Instance.UserVM.UserAccount.Id);
                await Navigation.PopAsync();
            }
        }
        //deselect the item
        IngredientView.SelectedItem = null;
    }
}