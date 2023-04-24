using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using MyFridge_UserInterface_MAUI.Views;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class RecipeViewModel : BindableObject
    {
        private readonly RecipeService _recipeService;
        private readonly IngredientAmountService _ingredientAmountService;
        private readonly CurrentUserService _currentUserService;
        private IEnumerable<RecipeDto> allRecipes;
        private IEnumerable<RecipeDto> makeableRecipes;
        private ObservableCollection<RecipeDetailViewModel> recipeDetails;
        public ObservableCollection<RecipeDetailViewModel> RecipeDetails
        {
            get => recipeDetails;
            private set
            {
                recipeDetails = value;
                OnPropertyChanged(nameof(RecipeDetails));
            }
        }
        public RecipeViewModel(RecipeService recipeService, 
            CurrentUserService currentUserService, 
            IngredientAmountService ingredientAmountService)
        {
            _recipeService = recipeService;
            _currentUserService = currentUserService;
            _ingredientAmountService = ingredientAmountService;
            recipeDetails = new();
        }
        public async Task RefreshRecipesAsync(bool filterToggle)
        {
            allRecipes = await _recipeService.GetAllAsync();
            makeableRecipes = await GetMakeableRecipesLazy();
            GetRecipesFilteredLazy(string.Empty, filterToggle);
        }
        public void GetRecipesFilteredLazy(string filter, bool filterToggle)
        {
            if (filterToggle)
            {
                if (string.IsNullOrEmpty(filter))
                    RecipeDetails = ToViewModel(allRecipes.OrderBy(recipe => recipe.Name).ToList());
                else
                    RecipeDetails = ToViewModel(allRecipes
                        .Where(recipe => recipe.Name.ToLower().StartsWith(filter))
                        .OrderBy(recipe => recipe.Name).ToList());
            }
            else
            {
                if (string.IsNullOrEmpty(filter))
                    RecipeDetails = ToViewModel(makeableRecipes.OrderBy(recipe => recipe.Name).ToList());
                else
                    RecipeDetails = ToViewModel(makeableRecipes
                        .Where(recipe => recipe.Name.ToLower().StartsWith(filter))
                        .OrderBy(recipe => recipe.Name).ToList());
            }
        }
        public async Task PushIngredientDetailAsync(INavigation nav, RecipeDetailViewModel detail)
        {
            await nav.PushAsync(new RecipeDetailPage(detail));
        }
        private ObservableCollection<RecipeDetailViewModel> ToViewModel(IEnumerable<RecipeDto> recipes)
        {
            ObservableCollection<RecipeDetailViewModel> viewModels = new();
            foreach (RecipeDto recipe in recipes)
            {
                RecipeDetailViewModel viewModel = new(_currentUserService, _ingredientAmountService)
                {
                    Recipe = recipe
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        private async Task<List<RecipeDto>> GetMakeableRecipesLazy()
        {
            UserAccountDto user = await _currentUserService.GetUserLazyAsync();

            return allRecipes.AsEnumerable()
                .Where(recipy => recipy.Ingredients
                    .All(recipyIngredient => user.Ingredients
                        .Any(userIngredient => recipyIngredient.Ingredient.Id == userIngredient.Ingredient.Id &&
                             recipyIngredient.Amount <= userIngredient.Amount)))
                .ToList();
        }
    }
}
