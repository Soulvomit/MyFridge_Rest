using MyFridge_UserInterface_MAUI.Mvvms.Service.Navigation.Interface;
using MyFridge_UserInterface_MAUI.Mvvms.ViewModel;
using MyFridge_UserInterface_MAUI.Mvvms.ViewModel.Detail;

namespace MyFridge_UserInterface_MAUI.Mvvms.View;

public partial class RecipePage : ContentPage
{
    private readonly RecipeViewModel _viewModel;
    private readonly INavigationService _navigationService;
    public RecipePage(INavigationService navigationService, RecipeViewModel viewModel)
    {
        InitializeComponent();

        _navigationService = navigationService;
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.RefreshAsync();
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
       _viewModel.GetFilteredLazy(e.NewTextValue.ToLower());
    }

    private void OnToggled(object sender, EventArgs e)
    {
        _viewModel.Toggle = !_viewModel.Toggle;
        _viewModel.GetFilteredLazy(string.Empty);
    }

    private async void OnRecipeSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is DetailRecipeViewModel selected)
            await _navigationService.GoToRecipeDetailAsync(selected.Recipe);
        //deselect the item
        (sender as CollectionView).SelectedItem = null;
    }
}