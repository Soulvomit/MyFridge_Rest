using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service.UoW.Interface;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel.Detail
{
    public class DetailRecipeViewModel : BindableObject
    {
        private readonly IUnitOfWork _uow;
        private ObservableCollection<DetailIngredientViewModel> recipeIngredientDetails;
        public ObservableCollection<DetailIngredientViewModel> RecipeIngredientDetails
        {
            get => recipeIngredientDetails;
            set
            {
                recipeIngredientDetails = value;
                OnPropertyChanged(nameof(RecipeIngredientDetails));
            }
        }
        public bool IsMakable
        {
            get
            {
                UserAccountDto user = _uow.UserClient.Lazy;
                return Recipe.Ingredients
                    .All(recipeIngredient => user.Ingredients
                        .Any(userIngredient => recipeIngredient.Ingredient.Id == userIngredient.Ingredient.Id &&
                                recipeIngredient.Amount <= userIngredient.Amount));
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

        public DetailRecipeViewModel(IUnitOfWork uow)
        {
            _uow = uow;

            recipeIngredientDetails = new ObservableCollection<DetailIngredientViewModel>();
        }

        public void RefreshRecipeIngredientsAsync()
        {
            RecipeIngredientDetails = ToViewModel(Recipe.Ingredients.OrderBy(i => i.Ingredient.Name));
            foreach (DetailIngredientViewModel ivm in RecipeIngredientDetails)
            {
                ivm.UpdateColorsAsync();
            }
        }
        private ObservableCollection<DetailIngredientViewModel> ToViewModel(IEnumerable<IngredientAmountDto> ingredients)
        {
            ObservableCollection<DetailIngredientViewModel> viewModels = new();
            foreach (IngredientAmountDto dto in ingredients)
            {
                DetailIngredientViewModel viewModel = new(_uow)
                {
                    IngredientAmount = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
