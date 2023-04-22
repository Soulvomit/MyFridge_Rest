using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class RecipyViewModel : INotifyPropertyChanged
    {
        public RecipyService RecipyService { get; private set; }
        public IngredientAmountService IAService { get; private set; }
        public CurrentUserService CUserService { get; private set; }
        public List<RecipyDto> all { get; set; }
        public List<RecipyDto> makeable { get; set; }
        private ObservableCollection<RecipyDetailViewModel> recipyDetails;
        public ObservableCollection<RecipyDetailViewModel> RecipyDetails
        {
            get => recipyDetails;
            private set
            {
                recipyDetails = value;
                OnPropertyChanged(nameof(RecipyDetails));
            }
        }
        public RecipyViewModel(RecipyService recipyService, CurrentUserService userService, IngredientAmountService iaService)
        {
            RecipyService = recipyService;
            CUserService = userService;
            IAService = iaService;
            all = new();
            makeable = new();
            recipyDetails = new();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task Refresh()
        {
            all = await RecipyService.GetAllAsync();
            makeable = await GetMakeableRecipiesLazy();
        }
        public void UpdateSearch(string text, bool filterToggle)
        {
            if (filterToggle)
            {
                if (string.IsNullOrEmpty(text))
                    RecipyDetails = ConvertRecipyDtos(all.OrderBy(recipy => recipy.Name).ToList());
                else
                    RecipyDetails = ConvertRecipyDtos(all
                        .Where(recipy => recipy.Name.ToLower().StartsWith(text))
                        .OrderBy(recipy => recipy.Name).ToList());
            }
            else
            {
                if (string.IsNullOrEmpty(text))
                    RecipyDetails = ConvertRecipyDtos(makeable.OrderBy(recipy => recipy.Name).ToList());
                else
                    RecipyDetails = ConvertRecipyDtos(makeable
                        .Where(recipy => recipy.Name.ToLower().StartsWith(text))
                        .OrderBy(recipy => recipy.Name).ToList());
            }
        }
        public void UpdateFilter(bool filterToggle)
        {
            if (!filterToggle)
                RecipyDetails = ConvertRecipyDtos(makeable.OrderBy(recipy => recipy.Name).ToList());
            else
                RecipyDetails = ConvertRecipyDtos(all.OrderBy(recipy => recipy.Name).ToList());

        }
        private ObservableCollection<RecipyDetailViewModel> ConvertRecipyDtos(List<RecipyDto> dtos)
        {
            ObservableCollection<RecipyDetailViewModel> viewModels = new();
            foreach (RecipyDto dto in dtos)
            {
                RecipyDetailViewModel viewModel = new(CUserService, IAService)
                {
                    Recipy = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        private async Task<List<RecipyDto>> GetMakeableRecipiesLazy()
        {
            UserAccountDto user = await CUserService.GetUserLazyAsync();

            return all.AsEnumerable()
                .Where(recipy => recipy.Ingredients
                    .All(recipyIngredient => user.Ingredients
                        .Any(userIngredient => recipyIngredient.Ingredient.Id == userIngredient.Ingredient.Id &&
                             recipyIngredient.Amount <= userIngredient.Amount)))
                .ToList();
        }
    }
}
