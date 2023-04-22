using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class IngredientViewModel : INotifyPropertyChanged
    {
        private List<IngredientDto> allIngredients;
        private ObservableCollection<IngredientDetailViewModel> ingredientDetails;
        //private readonly CurrentUserService _cUserService;
        public CurrentUserService _cUserService;
        private readonly IngredientService _ingredientService;
        public ObservableCollection<IngredientDetailViewModel> IngredientDetails
        {
            get => ingredientDetails;
            private set
            {
                ingredientDetails = value;
                OnPropertyChanged(nameof(IngredientDetails));
            }
        }
        public IngredientViewModel(CurrentUserService cUserService,
            IngredientService ingredientService)
        {
            _cUserService = cUserService;
            _ingredientService = ingredientService;
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
        public ObservableCollection<IngredientDetailViewModel> ConvertIngredientDtos(List<IngredientDto> dtos)
        {
            ObservableCollection<IngredientDetailViewModel> viewModels = new();
            foreach (IngredientDto dto in dtos)
            {
                IngredientDetailViewModel viewModel = new(_cUserService, _ingredientService)
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
