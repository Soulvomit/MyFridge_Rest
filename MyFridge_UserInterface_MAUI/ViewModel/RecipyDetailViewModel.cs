using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class RecipyDetailViewModel
    {
        private readonly RecipyService _recipyService;
        private readonly CurrentUserService _cUserService;
        public Color TextColor { get; set; } = Colors.White;
        public RecipyDto Recipy { get; set; }

        public RecipyDetailViewModel(RecipyService recipyService, CurrentUserService cUserService)
        {
            _recipyService = recipyService;
            _cUserService = cUserService;
        }

        public bool IsMakable(UserAccountDto user)
        {
            return this.Recipy.Ingredients
                .All(recipyIngredient => user.Ingredients
                    .Any(userIngredient => recipyIngredient.Id == userIngredient.Id &&
                            recipyIngredient.Amount <= userIngredient.Amount));
        }
        public List<UserIngredientDetailViewModel> ConvertIngredientDtos()
        {
            List<UserIngredientDetailViewModel> viewModels = new();
            foreach (IngredientDto dto in Recipy.Ingredients)
            {
                UserIngredientDetailViewModel viewModel = new(_cUserService)
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        public static List<RecipyDetailViewModel> ConvertRecipyDtos(
            List<RecipyDto> dtos, 
            CurrentUserService cUserService, 
            RecipyService recipyService)
        {
            List<RecipyDetailViewModel> viewModels = new();
            foreach (RecipyDto dto in dtos)
            {
                RecipyDetailViewModel viewModel = new(recipyService, cUserService)
                {
                    Recipy = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        public static async Task<List<RecipyDetailViewModel>> GetAllRecipiesFromDB(
            RecipyService recipyService, 
            CurrentUserService cUserService)
        {
            List<RecipyDto> dtos = await recipyService.GetRecipiesAsync();

            return ConvertRecipyDtos(dtos, cUserService, recipyService);
        }
        public async static Task<List<RecipyDetailViewModel>> GetMakeableRecipies(
            List<RecipyDetailViewModel> vms, 
            CurrentUserService cUserService)
        {
            UserAccountDto user = await cUserService.GetUserLazyAsync();
            return vms.AsEnumerable()
                .Where(vm => vm.Recipy.Ingredients
                    .All(recipyIngredient => user.Ingredients
                        .Any(userIngredient => recipyIngredient.Id == userIngredient.Id &&
                             recipyIngredient.Amount <= userIngredient.Amount)))
                .ToList();
        }
    }
}
