using MyFridge_Library_MAUI_Client.ClientModel;

namespace MyFridge_UserInterface_MAUI.Mvvms.Service.Navigation.Interface
{
    public interface INavigationService
    {
        public void SetDataStore(object data);
        public T GetDataStore<T>();
        public Task GoBack();
        public Task GoToIngredientDetailAsync(IngredientAmountDto ingredientAmount);
        public Task GoToRecipeDetailAsync(RecipeDto recipe);
        public Task GoToGroceriesAsync();
        public Task GoToUserAbsoluteAsync();
        public Task GoToLoginAbsoluteAsync();
    }
}
