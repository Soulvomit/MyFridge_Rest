using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class IngredientPage : ContentPage
{
    private readonly IngredientViewModel _viewModel;
    public IngredientPage(IngredientViewModel viewModel)
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
        if (e.CurrentSelection.FirstOrDefault() is IngredientDetailViewModel selectedIngredient)
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