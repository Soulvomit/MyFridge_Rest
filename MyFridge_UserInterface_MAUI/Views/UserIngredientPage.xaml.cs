using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserIngredientPage : ContentPage
{
    private readonly UserIngredientViewModel _viewModel;
    public UserIngredientPage(UserIngredientViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.RefreshUserIngredientsAsync();
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.GetUserIngredientsFilteredLazy(e.NewTextValue);
    }
    private async void OnIngredientSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is IngredientAmountDetailViewModel selected)
            await _viewModel.PushIngredientDetailAsync(Navigation, selected);

        //deselect the item
        (sender as CollectionView).SelectedItem = null;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await _viewModel.NavigateToGroceriesAsync();
    }
}