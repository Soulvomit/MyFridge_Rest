using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class RecipeDetailViewModel : BindableObject
    {
        private readonly CurrentUserService _currentUserService;
        private readonly IngredientAmountService _ingredientAmountService;
        private IEnumerable<IngredientAmountDto> recipeIngredients;
        private ObservableCollection<IngredientAmountDetailViewModel> recipeIngredientDetails;
        public ObservableCollection<IngredientAmountDetailViewModel> RecipeIngredientDetails
        {
            get => recipeIngredientDetails;
            set
            {
                recipeIngredientDetails = value;
                OnPropertyChanged(nameof(RecipeIngredientDetails));
            }
        }
        public RecipeDto Recipe { get; set; }
        public Color TextColor { get; set; } = Colors.White;
        public string Name 
        {
            get => Recipe.Name;
            private set
            {
                Recipe.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get => Recipe.Description;
            private set
            {
                Recipe.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public RecipeDetailViewModel(CurrentUserService currentUserService, IngredientAmountService ingredientAmountService)
        {
            _currentUserService = currentUserService;
            _ingredientAmountService = ingredientAmountService;
            recipeIngredientDetails = new ObservableCollection<IngredientAmountDetailViewModel>();
        }

        public async Task RefreshRecipeIngredientsAsync()
        {         
            recipeIngredients = Recipe.Ingredients;
            RecipeIngredientDetails = ToViewModel(recipeIngredients.OrderBy(i => i.Ingredient.Name));
            foreach (IngredientAmountDetailViewModel ivm in RecipeIngredientDetails)
            {
                await ivm.UpdateColorsAsync();
            }
        }

        public async Task<bool> IsMakable()
        {
            UserAccountDto user = await _currentUserService.GetUserLazyAsync();
            return Recipe.Ingredients
                .All(recipeIngredient => user.Ingredients
                    .Any(userIngredient => recipeIngredient.Ingredient.Id == userIngredient.Ingredient.Id &&
                            recipeIngredient.Amount <= userIngredient.Amount));
        }
        private ObservableCollection<IngredientAmountDetailViewModel> ToViewModel(IEnumerable<IngredientAmountDto> ingredients)
        {
            ObservableCollection<IngredientAmountDetailViewModel> viewModels = new();
            foreach (IngredientAmountDto dto in ingredients)
            {
                IngredientAmountDetailViewModel viewModel = new(_currentUserService, _ingredientAmountService)
                {
                    IngredientAmount = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
