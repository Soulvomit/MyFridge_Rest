using MyFridge_UserInterface_MAUI.Mvvms.Service.Navigation.Interface;
using MyFridge_UserInterface_MAUI.Mvvms.ViewModel;
using MyFridge_UserInterface_MAUI.Mvvms.ViewModel.Detail;

namespace MyFridge_UserInterface_MAUI.Mvvms.View;

public partial class GroceryPage : ContentPage
{
    private readonly GroceryViewModel _viewModel;
    private readonly INavigationService _navigationService;
    public GroceryPage(INavigationService navigationService, GroceryViewModel viewModel)
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
        _viewModel.GetFilteredLazy(e.NewTextValue);
    }
    private async void OnIngredientSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is DetailGroceryViewModel selectedIngredient)
        {
            string amountResult = await DisplayPromptAsync( "Add Amount", "Enter the amount", "OK", 
                                                            "Cancel", "0", -1, Keyboard.Numeric, "");
            await _viewModel.Add(selectedIngredient, amountResult);
            //navigate back
            await _navigationService.GoBack();
        }
        //deselect the item
        (sender as CollectionView).SelectedItem = null;
    }
}