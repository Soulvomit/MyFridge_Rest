using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using MyFridge_UserInterface_MAUI.Views;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserIngredientViewModel : BindableObject
    {
        private readonly IngredientAmountService _ingredientAmountService;
        private readonly CurrentUserService _cUserService;
        private IEnumerable<IngredientAmountDto> userIngredients;
        private ObservableCollection<IngredientAmountDetailViewModel> userIngredientDetails;
        public ObservableCollection<IngredientAmountDetailViewModel> UserIngredientDetails
        {
            get => userIngredientDetails;
            private set
            {
                userIngredientDetails = value;
                OnPropertyChanged(nameof(UserIngredientDetails));
            }
        }
        public UserIngredientViewModel(CurrentUserService cUserService, IngredientAmountService ingredientAmountService)
        {
            _cUserService = cUserService;
            _ingredientAmountService = ingredientAmountService;
            userIngredientDetails = new();
        }
        public async Task RefreshUserIngredientsAsync()
        {
            UserAccountDto user = await _cUserService.GetUserAsync();
            userIngredients = user.Ingredients;
            UserIngredientDetails = ToViewModel(userIngredients.OrderBy(i => i.Ingredient.Name));
        }

        public void GetUserIngredientsFilteredLazy(string filter)
        {
            if(string.IsNullOrEmpty(filter))
                UserIngredientDetails = ToViewModel(userIngredients.OrderBy(i => i.Ingredient.Name));
            else
                UserIngredientDetails = ToViewModel(userIngredients
                    .Where(i => i.Ingredient.Name.ToLower().StartsWith(filter.ToLower()))
                    .OrderBy(i => i.Ingredient.Name));
        }
        public async Task PushIngredientDetailAsync(INavigation nav, IngredientAmountDetailViewModel detail)
        {
            await nav.PushAsync(new UserIngredientDetailPage(detail));
        }
        public async Task NavigateToGroceriesAsync()
        {
            await Shell.Current.GoToAsync($"/" + nameof(IngredientPage));
        }
        private ObservableCollection<IngredientAmountDetailViewModel> ToViewModel(IEnumerable<IngredientAmountDto> ingredients)
        {
            ObservableCollection<IngredientAmountDetailViewModel> viewModels = new();
            foreach (IngredientAmountDto ingredient in ingredients)
            {
                IngredientAmountDetailViewModel viewModel = new(_cUserService, _ingredientAmountService)
                {
                    IngredientAmount = ingredient
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
