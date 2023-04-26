using MyFridge_UserInterface_MAUI.ViewModel;
using MyFridge_UserInterface_MAUI.ViewModel.Detail;

namespace MyFridge_UserInterface_MAUI.View;

public partial class GroceryPage : ContentPage
{
    private readonly GroceryViewModel _viewModel;
    public GroceryPage(GroceryViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.RefreshIngredientsAsync();
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.GetIngredientsFilteredLazy(e.NewTextValue);
    }
    private async void OnIngredientSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is DetailGroceryViewModel selectedIngredient)
        {
            string amountResult = await DisplayPromptAsync( "Add Amount", "Enter the amount", "OK", 
                                                            "Cancel", "0", -1, Keyboard.Numeric, "");
            await _viewModel.AddGrocery(selectedIngredient, amountResult);
            //navigate back
            await _viewModel.NavigateBack();
        }
        //deselect the item
        (sender as CollectionView).SelectedItem = null;
    }
}