using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserIngredientViewModel : INotifyPropertyChanged
    {
        //private readonly IngredientAmountService _iaService;
        //private readonly CurrentUserService _cUserService;
        //private readonly IngredientService _ingredientService;
        public IngredientAmountService _iaService;
        public CurrentUserService _cUserService;
        public IngredientService _ingredientService;
        private ObservableCollection<UserIngredientDetailViewModel> ingredientDetails;

        public ObservableCollection<UserIngredientDetailViewModel> IngredientDetails
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
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task GetIngredientDetailsAsync()
        {
            UserAccountDto user = await _cUserService.GetUserAsync();
            IngredientDetails = ConvertIngredientDtos(user.Ingredients.OrderBy(i => i.Name).ToList());
        }

        public async Task GetIngredientDetailsLazyAsync()
        {
            UserAccountDto user = await _cUserService.GetUserLazyAsync();
            IngredientDetails = ConvertIngredientDtos(user.Ingredients.OrderBy(i => i.Name).ToList());

        }

        public async Task GetIngredientDetailsFilteredLazyAsync(string filter)
        {
            UserAccountDto user = await _cUserService.GetUserLazyAsync();
            IngredientDetails = ConvertIngredientDtos(user.Ingredients
                .Where(i => i.Name.ToLower().StartsWith(filter.ToLower()))
                .OrderBy(i => i.Name).ToList());
        }
        public void UpdateDetails(List<UserIngredientDetailViewModel> newDetails)
        {
            IngredientDetails.Clear();
            foreach (var item in newDetails)
            {
                IngredientDetails.Add(item);
            }
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
