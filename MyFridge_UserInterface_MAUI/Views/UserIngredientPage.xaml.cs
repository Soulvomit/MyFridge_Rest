using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class UserIngredientPage : ContentPage
{
    private readonly UserIngredientViewModel _vm;
    public UserIngredientPage(UserIngredientViewModel vm)
    {
        InitializeComponent();

        _vm = vm;
        BindingContext = _vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _vm.GetDetailsAsync();
    }
    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            await _vm.GetDetailsLazyAsync();
        else
            await _vm.GetDetailsFilteredLazyAsync(e.NewTextValue);
    }
    private async void OnDetailSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is IngredientAmountDetailViewModel selectedDetail)
            await _vm.NavigateToDetailAsync(Navigation, selectedDetail);

        //deselect the item
        (sender as CollectionView).SelectedItem = null;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await _vm.NavigateToGroceriesAsync(Navigation);
    }
}