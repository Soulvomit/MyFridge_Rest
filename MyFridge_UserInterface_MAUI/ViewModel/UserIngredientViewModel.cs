using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using MyFridge_UserInterface_MAUI.Views;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserIngredientViewModel : BindableObject
    {
        private readonly IngredientAmountService _iaService;
        private readonly CurrentUserService _cUserService;
        private readonly IngredientService _ingredientService;
        private ObservableCollection<IngredientAmountDetailViewModel> ingredientDetails;

        public ObservableCollection<IngredientAmountDetailViewModel> IngredientDetails
        {
            get => ingredientDetails;
            private set
            {
                ingredientDetails = value;
                OnPropertyChanged(nameof(IngredientDetails));
            }
        }
        public UserIngredientViewModel(
            CurrentUserService cUserService,
            IngredientService ingredientService,
            IngredientAmountService iaService)
        {
            _cUserService = cUserService;
            _ingredientService = ingredientService;
            _iaService = iaService;
            ingredientDetails = new();
        }
        public async Task GetDetailsAsync()
        {
            UserAccountDto user = await _cUserService.GetUserAsync();
            IngredientDetails = ConvertToViewModel(user.Ingredients.OrderBy(i => i.Ingredient.Name));
        }

        public async Task GetDetailsLazyAsync()
        {
            UserAccountDto user = await _cUserService.GetUserLazyAsync();
            IngredientDetails = ConvertToViewModel(user.Ingredients.OrderBy(i => i.Ingredient.Name));

        }

        public async Task GetDetailsFilteredLazyAsync(string filter)
        {
            UserAccountDto user = await _cUserService.GetUserLazyAsync();
            IngredientDetails = ConvertToViewModel(user.Ingredients
                .Where(i => i.Ingredient.Name.ToLower().StartsWith(filter.ToLower()))
                .OrderBy(i => i.Ingredient.Name).ToList());
        }
        public void UpdateDetails(List<IngredientAmountDetailViewModel> newDetails)
        {
            IngredientDetails.Clear();
            foreach (var item in newDetails)
            {
                IngredientDetails.Add(item);
            }
        }
        public ObservableCollection<IngredientAmountDetailViewModel> ConvertToViewModel(IEnumerable<IngredientAmountDto> dtos)
        {
            ObservableCollection<IngredientAmountDetailViewModel> viewModels = new();
            foreach (IngredientAmountDto dto in dtos)
            {
                IngredientAmountDetailViewModel viewModel = new(_cUserService, _iaService)
                {
                    IngredientAmount = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        public async Task NavigateToDetailAsync(INavigation nav, IngredientAmountDetailViewModel detail)
        {
            await nav.PushAsync(new UserIngredientDetailPage(detail));
        }
        public async Task NavigateToGroceriesAsync(INavigation nav)
        {
            await nav.PushAsync(new IngredientPage(new IngredientViewModel(_cUserService, _ingredientService)));
        }
    }
}
