using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class RecipyPage : ContentPage
{
    private List<RecipyViewModel> all;
    private List<RecipyViewModel> makeable;
    public RecipyPage()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        all = await RecipyViewModel.GetAllRecipiesFromDB();
        makeable = RecipyViewModel.GetMakeableRecipies(all);

        OnRemoveFilterToggled(null, null);
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (IngredientFilterSwitch.IsToggled)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                RecipyView.ItemsSource = all.OrderBy(rvm => rvm.Recipy.Name);
            else
                RecipyView.ItemsSource = all
                    .Where(rvm => rvm.Recipy.Name.ToLower().StartsWith(e.NewTextValue.ToLower()))
                    .OrderBy(rvm => rvm.Recipy.Name);
        }
        else
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                RecipyView.ItemsSource = makeable.OrderBy(rvm => rvm.Recipy.Name);
            else
                RecipyView.ItemsSource = makeable
                    .Where(rvm => rvm.Recipy.Name.ToLower().StartsWith(e.NewTextValue.ToLower()))
                    .OrderBy(rvm => rvm.Recipy.Name);
        }
    }
    private void OnRemoveFilterToggled(object sender, EventArgs e)
    {
        if (!IngredientFilterSwitch.IsToggled)
            RecipyView.ItemsSource = makeable.OrderBy(rvm => rvm.Recipy.Name);
        else
            RecipyView.ItemsSource = all.OrderBy(rvm => rvm.Recipy.Name);
    }
    private async void OnRecipyTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is RecipyViewModel selectedRecipy)
            await Navigation.PushAsync(new RecipyDetailPage(selectedRecipy));
        //deselect the item
        RecipyView.SelectedItem = null;
    }
}