using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class IngredientViewModel : INotifyPropertyChanged
    {
        private List<IngredientDto> allIngredients;
        private ObservableCollection<UserIngredientDetailViewModel> ingredientDetails;
        //private readonly CurrentUserService _cUserService;
        public CurrentUserService _cUserService;
        private readonly IngredientService _ingredientService;
        private readonly IngredientAmountService _iaService;
        public ObservableCollection<UserIngredientDetailViewModel> IngredientDetails
        {
            get => ingredientDetails;
            private set
            {
                ingredientDetails = value;
                OnPropertyChanged(nameof(IngredientDetails));
            }
        }
        public IngredientViewModel(CurrentUserService cUserService,
            IngredientService ingredientService,
            IngredientAmountService iaService)
        {
            _cUserService = cUserService;
            _ingredientService = ingredientService;
            _iaService = iaService;
            ingredientDetails = new();
            allIngredients = new();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task GetIngredientDetailsAsync()
        {
            allIngredients = await _ingredientService.GetAllAsync();
            IngredientDetails = ConvertIngredientDtos(allIngredients.OrderBy(i => i.Name).ToList());
        }
        public void GetIngredientDetailsLazyAsync()
        {
            IngredientDetails =
                ConvertIngredientDtos(allIngredients.OrderBy(i => i.Name).ToList());
        }

        public void GetIngredientDetailsFilteredLazyAsync(string filter)
        {
            IngredientDetails = ConvertIngredientDtos(allIngredients
                .Where(i => i.Name.ToLower().StartsWith(filter.ToLower()))
                .OrderBy(i => i.Name).ToList());
        }
        public ObservableCollection<UserIngredientDetailViewModel> ConvertIngredientDtos(List<IngredientDto> dtos)
        {
            ObservableCollection<UserIngredientDetailViewModel> viewModels = new();
            foreach (IngredientDto dto in dtos)
            {
                UserIngredientDetailViewModel viewModel = new(_cUserService, _iaService)
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
