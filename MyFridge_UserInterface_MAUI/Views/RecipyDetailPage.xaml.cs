using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Views;

public partial class RecipyDetailPage : ContentPage
{
	private readonly RecipyViewModel _vm; 
	public RecipyDetailPage(RecipyViewModel vm)
	{
		InitializeComponent();

		_vm = vm;
        BindingContext = _vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        Name.Text = _vm.Recipy.Name;
        Description.Text = _vm.Recipy.Description;

        List<IngredientViewModel> ingredientsVms = _vm.ConvertIngredientDtos();

        foreach(IngredientViewModel ivm in ingredientsVms)
        {
            ivm.SetColor();
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