using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service.UoW.Interface;
using MyFridge_UserInterface_MAUI.View;
using MyFridge_UserInterface_MAUI.View.Detail;
using MyFridge_UserInterface_MAUI.ViewModel.Detail;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserIngredientViewModel : BindableObject
    {
        private readonly IUnitOfWork _uow;
        private ObservableCollection<DetailIngredientViewModel> userIngredientDetails;
        public ObservableCollection<DetailIngredientViewModel> UserIngredientDetails
        {
            get => userIngredientDetails;
            private set
            {
                userIngredientDetails = value;
                OnPropertyChanged(nameof(UserIngredientDetails));
            }
        }
        public UserIngredientViewModel(IUnitOfWork uow)
        {
            _uow = uow;
            userIngredientDetails = new();
        }
        public async Task RefreshUserIngredientsAsync()
        {
            UserAccountDto user = await _uow.UserClient.GetAsync(_uow.UserClient.Lazy.Id);
            UserIngredientDetails = ToViewModel(user.Ingredients.OrderBy(i => i.Ingredient.Name));
        }

        public void GetUserIngredientsFilteredLazy(string filter)
        {
            if(string.IsNullOrEmpty(filter))
                UserIngredientDetails = ToViewModel(_uow.UserClient.Lazy.Ingredients
                    .OrderBy(i => i.Ingredient.Name));
            else
                UserIngredientDetails = ToViewModel(_uow.UserClient.Lazy.Ingredients
                    .Where(i => i.Ingredient.Name.ToLower().StartsWith(filter.ToLower()))
                    .OrderBy(i => i.Ingredient.Name));
        }
        public async Task PushIngredientDetailAsync(INavigation nav, DetailIngredientViewModel detail)
        {
            await nav.PushAsync(new DetailUserIngredientPage(detail));
        }
        public async Task NavigateToGroceriesAsync()
        {
            await Shell.Current.GoToAsync($"/" + nameof(GroceryPage));
        }
        private ObservableCollection<DetailIngredientViewModel> ToViewModel(IEnumerable<IngredientAmountDto> ingredients)
        {
            ObservableCollection<DetailIngredientViewModel> viewModels = new();
            foreach (IngredientAmountDto ingredient in ingredients)
            {
                DetailIngredientViewModel viewModel = new(_uow)
                {
                    IngredientAmount = ingredient
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
