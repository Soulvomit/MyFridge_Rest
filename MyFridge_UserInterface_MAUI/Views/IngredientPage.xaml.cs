using MyFridge_UserInterface_MAUI.Service;
using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class IngredientPage : ContentPage
{
    private readonly CurrentUserService _cUserService;
    private readonly IngredientService _ingredientService;
    private List<UserIngredientDetailViewModel> ingredients;
    public IngredientPage(IngredientService ingredientService, CurrentUserService cUserService)
    {
        InitializeComponent();

        _ingredientService = ingredientService;
        _cUserService = cUserService;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();


        ingredients = UserIngredientDetailViewModel
            .ConvertIngredientDtos(await _ingredientService.GetIngredientsLazy(), _cUserService);
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
        if (e.Item is UserIngredientDetailViewModel selectedIngredient)
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
                await _cUserService.UserClient
                    .AddIngredientAsync(selectedIngredient.Ingredient, _cUserService.CurrentUserId);
                await Navigation.PopAsync();
            }
        }
        //deselect the item
        IngredientView.SelectedItem = null;
    }
}