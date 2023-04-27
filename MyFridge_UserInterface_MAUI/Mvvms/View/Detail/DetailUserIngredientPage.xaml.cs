using MyFridge_Library_MAUI_Client.ClientModel;
using MyFridge_UserInterface_MAUI.Mvvms.Service.Navigation.Interface;
using MyFridge_UserInterface_MAUI.Mvvms.ViewModel.Detail;

namespace MyFridge_UserInterface_MAUI.Mvvms.View.Detail;

public partial class DetailUserIngredientPage : ContentPage
{
    private readonly DetailIngredientViewModel _viewModel;
    private readonly INavigationService _navigationService;
    public DetailUserIngredientPage(INavigationService navigationService, DetailIngredientViewModel viewModel)
    {
        InitializeComponent();

        _navigationService = navigationService;
        _viewModel = viewModel;
        _viewModel.IngredientAmount = _navigationService.GetDataStore<IngredientAmountDto>();
        BindingContext = _viewModel;
    }
    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        await _viewModel.SaveAsync();
        await _navigationService.GoBack();
    }
    private void OnAmountCompleted(object sender, EventArgs e)
    {
        (sender as Entry).Unfocus();
    }
}