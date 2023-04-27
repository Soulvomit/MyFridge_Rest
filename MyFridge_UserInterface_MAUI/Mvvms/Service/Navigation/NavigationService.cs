using MyFridge_Library_MAUI_Client.ClientModel;
using MyFridge_UserInterface_MAUI.Mvvms.Service.Navigation.Interface;
using MyFridge_UserInterface_MAUI.Mvvms.View;
using MyFridge_UserInterface_MAUI.Mvvms.View.Detail;

namespace MyFridge_UserInterface_MAUI.Mvvms.Service.Navigation
{
    public class NavigationService : INavigationService
    {
        private object _dataStore;

        public void SetDataStore(object data)
        {
            _dataStore = data;
        }
        public T GetDataStore<T>()
        {
            return (T)_dataStore;
        }
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }
        public async Task GoToIngredientDetailAsync(IngredientAmountDto ingredientAmount)
        {
            SetDataStore(ingredientAmount);
            await Shell.Current.GoToAsync(nameof(DetailUserIngredientPage));
        }
        public async Task GoToRecipeDetailAsync(RecipeDto recipe)
        {
            SetDataStore(recipe);
            await Shell.Current.GoToAsync(nameof(DetailRecipePage));
        }
        public async Task GoToGroceriesAsync()
        {
            await Shell.Current.GoToAsync(nameof(GroceryPage));
        }
        public async Task GoToUserAbsoluteAsync()
        {
            await Shell.Current.GoToAsync($"//" + nameof(UserInfoPage));
        }
        public async Task GoToLoginAbsoluteAsync()
        {
            await Shell.Current.GoToAsync($"//" + nameof(UserLoginPage));
        }
    }
}
