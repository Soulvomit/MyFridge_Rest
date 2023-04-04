using MyFridge_UserInterface_MAUI.Service;
using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserIngredientPage : ContentPage
{
    private readonly UserService _userService;
    public UserIngredientPage(UserService userService)
    {
        InitializeComponent();

        _userService = userService;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _userService.User = await _userService.UserClient.GetUserAccountAsync(_userService.User.Id);
        IngredientView.ItemsSource = 
            IngredientViewModel.ConvertIngredientDtos(
                    _userService.User.Ingredients.OrderBy(i => i.Name).ToList(), _userService);
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            IngredientView.ItemsSource =
                IngredientViewModel.ConvertIngredientDtos(
                        _userService.User.Ingredients.OrderBy(i => i.Name).ToList(), _userService);
        else
            IngredientView.ItemsSource = 
                IngredientViewModel.ConvertIngredientDtos(
                        _userService.User.Ingredients
                            .Where(i => i.Name.ToLower().StartsWith(e.NewTextValue.ToLower()))
                                .OrderBy(i => i.Name).ToList(), _userService);
    }
    private async void OnIngredientTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is IngredientViewModel selectedIngredient)
            await Navigation.PushAsync(new UserIngredientDetailPage(selectedIngredient));
        //deselect the item
        IngredientView.SelectedItem = null;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new IngredientPage(_userService));
    }
}