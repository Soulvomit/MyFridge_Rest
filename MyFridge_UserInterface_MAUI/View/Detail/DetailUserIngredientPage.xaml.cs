using MyFridge_UserInterface_MAUI.ViewModel.Detail;

namespace MyFridge_UserInterface_MAUI.View.Detail;

public partial class DetailUserIngredientPage : ContentPage
{
    private readonly DetailIngredientViewModel _viewModel;
    public DetailUserIngredientPage(DetailIngredientViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        await _viewModel.RefreshAndSaveAsync();
        await _viewModel.NavigateBack();
    }
    private void OnAmountCompleted(object sender, EventArgs e)
    {
        (sender as Entry).Unfocus();
    }
}