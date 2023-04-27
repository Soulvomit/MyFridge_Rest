using MyFridge_Library_Client_MAUI.ClientModel;
using MyFridge_Interface_MAUI.Mvvms.Service.Client.Interface;
using System.Collections.ObjectModel;

namespace MyFridge_Interface_MAUI.Mvvms.ViewModel.Detail
{
    public class DetailRecipeViewModel : BindableObject
    {
        #region Privates
        private readonly IClientService _clientService;
        private ObservableCollection<DetailIngredientViewModel> recipeIngredientDetails;
        #endregion

        #region Properties
        public RecipeDto Recipe { get; set; }
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
                UserAccountDto user = _clientService.UserClient.Lazy;
                return Recipe.IngredientAmounts
                    .All(recipeIngredient => user.IngredientAmounts
                        .Any(userIngredient => recipeIngredient.Ingredient.Id == userIngredient.Ingredient.Id &&
                                recipeIngredient.Amount <= userIngredient.Amount));
            }
        }
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
        public string Method
        {
            get => Recipe.Method;
            private set
            {
                Recipe.Method = value;
                OnPropertyChanged(nameof(Method));
            }
        }
        #endregion

        public DetailRecipeViewModel(IClientService clientService)
        {
            _clientService = clientService;

            recipeIngredientDetails = new ObservableCollection<DetailIngredientViewModel>();
        }

        public void RefreshAsync()
        {
            RecipeIngredientDetails = ToViewModel(Recipe.IngredientAmounts.OrderBy(i => i.Ingredient.Name));
            foreach (DetailIngredientViewModel ivm in RecipeIngredientDetails)
            {
                ivm.Update();
            }
        }
        private ObservableCollection<DetailIngredientViewModel> ToViewModel(IEnumerable<IngredientAmountDto> ingredients)
        {
            ObservableCollection<DetailIngredientViewModel> viewModels = new();
            foreach (IngredientAmountDto dto in ingredients)
            {
                DetailIngredientViewModel viewModel = new(_clientService)
                {
                    IngredientAmount = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
