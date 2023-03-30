using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserIngredientPage : ContentPage
{
    public UserIngredientPage()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        UserService.Instance.UserVM.UserAccount = 
            await UserService.Instance.UserClient.GetUserAccountAsync(
                UserService.Instance.UserVM.UserAccount.Id);
        IngredientView.ItemsSource = 
            UserService.Instance.UserVM.UserAccount.Ingredients.OrderBy(i => i.Name).ToList();
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            IngredientView.ItemsSource = 
                UserService.Instance.UserVM.UserAccount.Ingredients.OrderBy(i => i.Name);
        else
            IngredientView.ItemsSource = 
                UserService.Instance.UserVM.UserAccount.Ingredients
                .Where(i => i.Name.ToLower().StartsWith(e.NewTextValue.ToLower()))
                .OrderBy(i => i.Name);
    }
    private async void OnIngredientTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is IngredientDto selectedIngredient)
            await Navigation.PushAsync(new UserIngredientDetailPage(selectedIngredient));
        //deselect the item
        IngredientView.SelectedItem = null;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new IngredientPage());
    }
}