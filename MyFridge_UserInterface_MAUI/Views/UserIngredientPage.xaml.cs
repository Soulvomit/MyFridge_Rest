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

        _vm.UpdateDetails(await _vm.GetIngredientDetailsAsync());
    }
    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            _vm.UpdateDetails(await _vm.GetIngredientDetailsLazyAsync());
        else
            _vm.UpdateDetails(await _vm.GetIngredientDetailsFilteredLazyAsync(e.NewTextValue));
    }
    private async void OnIngredientSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is UserIngredientDetailViewModel selectedIngredient)
            await Navigation.PushAsync(new UserIngredientDetailPage(selectedIngredient));

        //deselect the item
        //IngredientView.SelectedItem = null;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new IngredientPage(_vm.IngredientService, _vm.CUserService));
    }
}