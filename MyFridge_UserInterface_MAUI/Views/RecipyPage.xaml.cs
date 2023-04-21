using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class RecipyPage : ContentPage
{
    private readonly RecipyViewModel _vm;
    public RecipyPage(RecipyViewModel vm)
    {
        InitializeComponent();

        _vm = vm;
        BindingContext = _vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _vm.Refresh();

        OnRemoveFilterToggled(null, null);
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _vm.UpdateSearch(e.NewTextValue.ToLower(), IngredientFilterSwitch.IsToggled);
    }

    private void OnRemoveFilterToggled(object sender, EventArgs e)
    {
        _vm.UpdateFilter(IngredientFilterSwitch.IsToggled);
    }

    private async void OnRecipySelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is RecipyDetailViewModel selectedRecipy)
            await Navigation.PushAsync(new RecipyDetailPage(selectedRecipy));
        //deselect the item
        (sender as CollectionView).SelectedItem = null;
    }
}