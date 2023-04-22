using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class RecipyDetailViewModel : INotifyPropertyChanged
    {
        private readonly CurrentUserService _cUserService;
        private readonly IngredientAmountService _iaService;

        private ObservableCollection<IngredientAmountDetailViewModel> ingredientDetails;
        public ObservableCollection<IngredientAmountDetailViewModel> IngredientDetails
        {
            get => ingredientDetails;
            set
            {
                ingredientDetails = value;
                OnPropertyChanged(nameof(IngredientDetails));
            }
        }

        public Color TextColor { get; set; } = Colors.White;
        public RecipyDto Recipy { get; set; }
        public string Name 
        {
            get => Recipy.Name;
            private set
            {
                Recipy.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get => Recipy.Description;
            private set
            {
                Recipy.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public RecipyDetailViewModel(
            CurrentUserService cUserService,
            IngredientAmountService iaService)
        {
            _cUserService = cUserService;
            _iaService = iaService;
            ingredientDetails = new ObservableCollection<IngredientAmountDetailViewModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsMakable(UserAccountDto user)
        {
            return Recipy.Ingredients
                .All(recipyIngredient => user.Ingredients
                    .Any(userIngredient => recipyIngredient.Ingredient.Id == userIngredient.Ingredient.Id &&
                            recipyIngredient.Amount <= userIngredient.Amount));
        }
        public List<IngredientAmountDetailViewModel> ConvertIngredientDtos()
        {
            List<IngredientAmountDetailViewModel> viewModels = new();
            foreach (IngredientAmountDto dto in Recipy.Ingredients)
            {
                IngredientAmountDetailViewModel viewModel = new(_cUserService, _iaService)
                {
                    IngredientAmount = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
