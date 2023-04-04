using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class RecipyViewModel
    {
        private readonly UserService _userService;
        public Color TextColor { get; set; } = Colors.White;
        public RecipyDto Recipy { get; set; }
        public bool IsMakable 
        {
            get 
            {
                return this.Recipy.Ingredients
                    .All(recipyIngredient => _userService.User.Ingredients
                        .Any(userIngredient => recipyIngredient.Id == userIngredient.Id &&
                             recipyIngredient.Amount <= userIngredient.Amount));
            } 
        }

        public RecipyViewModel(UserService userService)
        {
            _userService = userService;
        }

        public List<IngredientViewModel> ConvertIngredientDtos()
        {
            List<IngredientViewModel> viewModels = new();
            foreach (IngredientDto dto in Recipy.Ingredients)
            {
                IngredientViewModel viewModel = new(_userService)
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        public static List<RecipyViewModel> ConvertRecipyDtos(List<RecipyDto> dtos, UserService userService)
        {
            List<RecipyViewModel> viewModels = new();
            foreach (RecipyDto dto in dtos)
            {
                RecipyViewModel viewModel = new(userService)
                {
                    Recipy = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        public static async Task<List<RecipyViewModel>> GetAllRecipiesFromDB(UserService userService)
        {
            List<RecipyDto> dtos = await userService.RecipyClient.GetAllAsync();

            return ConvertRecipyDtos(dtos, userService);
        }
        public static List<RecipyViewModel> GetMakeableRecipies(List<RecipyViewModel> vms, UserService userService)
        {
            return vms.AsEnumerable()
                .Where(vm => vm.Recipy.Ingredients
                    .All(recipyIngredient => userService.User.Ingredients
                        .Any(userIngredient => recipyIngredient.Id == userIngredient.Id &&
                             recipyIngredient.Amount <= userIngredient.Amount)))
                .ToList();
        }
    }
}
