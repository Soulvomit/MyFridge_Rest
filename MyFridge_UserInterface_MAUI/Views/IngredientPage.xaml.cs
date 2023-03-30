using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class IngredientPage : ContentPage
{
    private List<IngredientDto> ingredients;
    public IngredientPage()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        ingredients = await UserService.Instance.IngredientClient.GetAllAsync();
        IngredientView.ItemsSource = ingredients.OrderBy(i => i.Name);
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            IngredientView.ItemsSource = ingredients.OrderBy(i => i.Name);
        else
            IngredientView.ItemsSource = ingredients
                .Where(i => i.Name.ToLower()
                                  .StartsWith(e.NewTextValue
                                  .ToLower()))
                .OrderBy(i => i.Name);
    }
    private async void OnIngredientTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is IngredientDto selectedIngredient)
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
                selectedIngredient.Amount = amount;
                await UserService.Instance.UserClient.AddIngredientAsync(selectedIngredient, UserService.Instance.UserVM.UserAccount.Id);
                await Navigation.PopAsync();
            }
        }
        //deselect the item
        IngredientView.SelectedItem = null;
    }
}