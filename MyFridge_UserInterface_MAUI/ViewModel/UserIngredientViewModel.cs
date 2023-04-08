using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserIngredientViewModel
    {
        public CurrentUserService CUserService { get; private set; }
        public IngredientService IngredientService { get; private set; }
        public ObservableCollection<UserIngredientDetailViewModel> IngredientDetails { get; private set; }
        public UserIngredientViewModel(CurrentUserService cUserService, IngredientService ingredientService)
        {
            CUserService = cUserService;
            IngredientService = ingredientService;
            IngredientDetails = new();
        }
        public async Task<List<UserIngredientDetailViewModel>> GetIngredientDetailsAsync()
        {
            UserAccountDto user = await CUserService.GetUserAsync();
            return ConvertIngredientDtos(user.Ingredients.OrderBy(i => i.Name).ToList());
        }

        public async Task<List<UserIngredientDetailViewModel>> GetIngredientDetailsLazyAsync()
        {
            UserAccountDto user = await CUserService.GetUserLazyAsync();
            return ConvertIngredientDtos(user.Ingredients.OrderBy(i => i.Name).ToList());

        }

        public async Task<List<UserIngredientDetailViewModel>> GetIngredientDetailsFilteredLazyAsync(string filter)
        {
            UserAccountDto user = await CUserService.GetUserLazyAsync();
            return ConvertIngredientDtos(user.Ingredients
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
        public List<UserIngredientDetailViewModel> ConvertIngredientDtos(List<IngredientDto> dtos)
        {
            List<UserIngredientDetailViewModel> viewModels = new();
            foreach (IngredientDto dto in dtos)
            {
                UserIngredientDetailViewModel viewModel = new(CUserService)
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
