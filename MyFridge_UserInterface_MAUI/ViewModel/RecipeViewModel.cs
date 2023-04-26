using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service.UoW.Interface;
using MyFridge_UserInterface_MAUI.View.Detail;
using MyFridge_UserInterface_MAUI.ViewModel.Detail;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class RecipeViewModel : BindableObject
    {
        private readonly IUnitOfWork _uow;
        private ObservableCollection<DetailRecipeViewModel> recipeDetails;
        public ObservableCollection<DetailRecipeViewModel> RecipeDetails
        {
            get => recipeDetails;
            private set
            {
                recipeDetails = value;
                OnPropertyChanged(nameof(RecipeDetails));
            }
        }
        public RecipeViewModel(IUnitOfWork uow)
        {
            _uow = uow;

            recipeDetails = new();
        }
        public async Task RefreshRecipesAsync(bool filterToggle)
        {
            await _uow.RecipeClient.GetAllAsync();
             
            GetRecipesFilteredLazy(string.Empty, filterToggle);
        }
        public async Task GetRecipesFilteredLazy(string filter, bool filterToggle)
        {
            IEnumerable<RecipeDto> showList;

            if (filterToggle) showList = _uow.RecipeClient.AllLazies;
            else  showList = await GetMakeableRecipesLazy();

            if (string.IsNullOrEmpty(filter))
                RecipeDetails = ToViewModel(showList.OrderBy(recipe => recipe.Name).ToList());
            else
                //RecipeDetails = ToViewModel(showList
                //    .Where(recipe => recipe.Name.ToLower().StartsWith(filter))
                //    .OrderBy(recipe => recipe.Name).ToList());
                RecipeDetails = ToViewModel(await _uow.RecipeClient.GetFilteredAsync(filter));
        }
        public async Task PushIngredientDetailAsync(INavigation nav, DetailRecipeViewModel detail)
        {
            await nav.PushAsync(new DetailRecipePage(detail));
        }
        private ObservableCollection<DetailRecipeViewModel> ToViewModel(IEnumerable<RecipeDto> recipes)
        {
            ObservableCollection<DetailRecipeViewModel> viewModels = new();
            foreach (RecipeDto recipe in recipes)
            {
                DetailRecipeViewModel viewModel = new(_uow)
                {
                    Recipe = recipe
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        private async Task<List<RecipeDto>> GetMakeableRecipesLazy()
        {
            UserAccountDto user = _uow.UserClient.Lazy;

            return _uow.RecipeClient.AllLazies.AsEnumerable()
                .Where(recipy => recipy.Ingredients
                    .All(recipyIngredient => user.Ingredients
                        .Any(userIngredient => recipyIngredient.Ingredient.Id == userIngredient.Ingredient.Id &&
                             recipyIngredient.Amount <= userIngredient.Amount)))
                .ToList();
        }
    }
}
