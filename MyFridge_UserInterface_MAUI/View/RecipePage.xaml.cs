using MyFridge_UserInterface_MAUI.ViewModel;
using MyFridge_UserInterface_MAUI.ViewModel.Detail;

namespace MyFridge_UserInterface_MAUI.View;

public partial class RecipePage : ContentPage
{
    private readonly RecipeViewModel _viewModel;
    public RecipePage(RecipeViewModel vm)
    {
        InitializeComponent();

        _viewModel = vm;
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.RefreshRecipesAsync(IngredientFilterSwitch.IsToggled);
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.GetRecipesFilteredLazy(e.NewTextValue.ToLower(), IngredientFilterSwitch.IsToggled);
    }

    private void OnRemoveFilterToggled(object sender, EventArgs e)
    {
        _viewModel.GetRecipesFilteredLazy(string.Empty, IngredientFilterSwitch.IsToggled);
    }

    private async void OnRecipeSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is DetailRecipeViewModel selected)
            await _viewModel.PushIngredientDetailAsync(Navigation, selected);
        //deselect the item
        (sender as CollectionView).SelectedItem = null;
    }
}