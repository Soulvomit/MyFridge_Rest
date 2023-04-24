using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class IngredientViewModel : BindableObject
    {
        private readonly CurrentUserService _currentUserService;
        private readonly IngredientService _ingredientService;
        private IEnumerable<IngredientDto> ingredients;
        private ObservableCollection<IngredientDetailViewModel> ingredientDetails;
        public ObservableCollection<IngredientDetailViewModel> IngredientDetails
        {
            get => ingredientDetails;
            private set
            {
                ingredientDetails = value;

                OnPropertyChanged(nameof(IngredientDetails));
            }
        }
        public IngredientViewModel(CurrentUserService currentUserService, IngredientService ingredientService)
        {
            _currentUserService = currentUserService;
            _ingredientService = ingredientService;
            ingredientDetails = new();
        }
        public async Task RefreshIngredientsAsync()
        {
            ingredients = await _ingredientService.GetAllAsync();
            IngredientDetails = ToViewModel(ingredients.OrderBy(i => i.Name).ToList());
        }
        public void GetIngredientsFilteredLazy(string filter)
        {
            if (string.IsNullOrEmpty(filter)) 
                IngredientDetails = ToViewModel(ingredients.OrderBy(i => i.Name).ToList());
            else
                IngredientDetails = ToViewModel(ingredients
                    .Where(i => i.Name.ToLower().StartsWith(filter.ToLower()))
                    .OrderBy(i => i.Name).ToList());
        }
        public async Task AddGrocery(IngredientDetailViewModel ingredient, string amountResult)
        {
            bool parsed = uint.TryParse(amountResult, out uint amount);
            if (parsed)
            {
                IngredientAmountDto dto = new()
                {
                    Ingredient = ingredient.Ingredient,
                    Amount = amount
                };
                await _currentUserService.Client.AddIngredientAsync(dto, _currentUserService.CurrentUserId);
            }
        }
        public async Task NavigateBack()
        {
            await Shell.Current.GoToAsync($"..");
        }
        private ObservableCollection<IngredientDetailViewModel> ToViewModel(IEnumerable<IngredientDto> ingredients)
        {
            ObservableCollection<IngredientDetailViewModel> viewModels = new();
            foreach (IngredientDto dto in ingredients)
            {
                IngredientDetailViewModel viewModel = new()
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
