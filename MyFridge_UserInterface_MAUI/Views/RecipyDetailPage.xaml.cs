using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class RecipyDetailPage : ContentPage
{
	private readonly RecipyDetailViewModel _vm; 
	public RecipyDetailPage(RecipyDetailViewModel vm)
	{
		InitializeComponent();

		_vm = vm;
        BindingContext = _vm;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        List<IngredientAmountDetailViewModel> list = _vm.ConvertIngredientDtos()
            .OrderBy(ivm => ivm.IngredientAmount.Ingredient.Name)
            .ToList();
        foreach (IngredientAmountDetailViewModel ivm in list)
        {
            await ivm.UpdateColorAsync();
            _vm.IngredientDetails.Add(ivm);
        }
    }
    private async void OnDetailSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is IngredientAmountDetailViewModel selectedIngredient)
        {
            //await Navigation.PushAsync(new UserIngredientDetailPage(selectedIngredient));
        }
        //deselect the item
        (sender as CollectionView).SelectedItem = null;
    }
}