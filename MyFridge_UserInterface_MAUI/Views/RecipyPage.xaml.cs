using MyFridge_UserInterface_MAUI.Service;
using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class RecipyPage : ContentPage
{
    RecipyViewModel _vm;
    public RecipyPage(RecipyViewModel vm)
    {
        InitializeComponent();

        _vm = vm;
        BindingContext = _vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _vm.All = await RecipyDetailViewModel.GetAllRecipiesFromDB(_vm.RecipyService, _vm.CUserService);
        _vm.Makeable = await RecipyDetailViewModel.GetMakeableRecipies(_vm.All, _vm.CUserService);

        OnRemoveFilterToggled(null, null);
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (IngredientFilterSwitch.IsToggled)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                RecipyView.ItemsSource = _vm.All.OrderBy(rvm => rvm.Recipy.Name);
            else
                RecipyView.ItemsSource = _vm.All
                    .Where(rvm => rvm.Recipy.Name.ToLower().StartsWith(e.NewTextValue.ToLower()))
                    .OrderBy(rvm => rvm.Recipy.Name);
        }
        else
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                RecipyView.ItemsSource = _vm.Makeable.OrderBy(rvm => rvm.Recipy.Name);
            else
                RecipyView.ItemsSource = _vm.Makeable
                    .Where(rvm => rvm.Recipy.Name.ToLower().StartsWith(e.NewTextValue.ToLower()))
                    .OrderBy(rvm => rvm.Recipy.Name);
        }
    }
    private void OnRemoveFilterToggled(object sender, EventArgs e)
    {
        if (!IngredientFilterSwitch.IsToggled)
            RecipyView.ItemsSource = _vm.Makeable.OrderBy(rvm => rvm.Recipy.Name);
        else
            RecipyView.ItemsSource = _vm.All.OrderBy(rvm => rvm.Recipy.Name);
    }
    private async void OnRecipyTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is RecipyDetailViewModel selectedRecipy)
            await Navigation.PushAsync(new RecipyDetailPage(selectedRecipy));
        //deselect the item
        RecipyView.SelectedItem = null;
    }
}