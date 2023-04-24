using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class RecipeDetailPage : ContentPage
{
	private readonly RecipeDetailViewModel _viewModel; 
	public RecipeDetailPage(RecipeDetailViewModel viewModel)
	{
		InitializeComponent();

		_viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.RefreshRecipeIngredientsAsync();
    }
    private async void OnIngredientSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is IngredientAmountDetailViewModel selected)
        {
            //await Navigation.PushAsync(new UserIngredientDetailPage(selected));
        }
        //deselect the item
        (sender as CollectionView).SelectedItem = null;
    }
}