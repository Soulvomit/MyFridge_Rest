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

        Name.Text = _vm.Recipy.Name;
        Description.Text = _vm.Recipy.Description;

        List<UserIngredientDetailViewModel> ingredientsVms = _vm.ConvertIngredientDtos();

        foreach(UserIngredientDetailViewModel ivm in ingredientsVms)
        {
            await ivm.SetColor();
        }

        IngredientView.ItemsSource = ingredientsVms
            .OrderBy(ivm => ivm.Ingredient.Name)
            .ToList();
    }
    private async void OnIngredientTapped(object sender, ItemTappedEventArgs e)
    {
        //if (e.Item is IngredientDto selectedIngredient)
            //await Navigation.PushAsync(new UserIngredientDetailPage(selectedIngredient));
        //deselect the item
        IngredientView.SelectedItem = null;
    }
}