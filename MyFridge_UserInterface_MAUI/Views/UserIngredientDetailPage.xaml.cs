using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserIngredientDetailPage : ContentPage
{
    private readonly IngredientAmountDetailViewModel _viewModel;
    public UserIngredientDetailPage(IngredientAmountDetailViewModel viewModel)
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