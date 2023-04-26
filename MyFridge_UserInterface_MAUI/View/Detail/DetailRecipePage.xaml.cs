using MyFridge_UserInterface_MAUI.ViewModel.Detail;

namespace MyFridge_UserInterface_MAUI.View.Detail;

public partial class DetailRecipePage : ContentPage
{
	private readonly DetailRecipeViewModel _viewModel; 
	public DetailRecipePage(DetailRecipeViewModel viewModel)
	{
		InitializeComponent();

		_viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.RefreshRecipeIngredientsAsync();
    }
    private async void OnIngredientSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is DetailIngredientViewModel selected)
        {
            //await Navigation.PushAsync(new UserIngredientDetailPage(selected));
        }
        //deselect the item
        (sender as CollectionView).SelectedItem = null;
    }
}