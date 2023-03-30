using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class RecipyViewModel
    {
        public Color TextColor { get; set; } = Colors.White;
        public RecipyDto Recipy { get; set; }
        public bool IsMakable 
        {
            get 
            {
                return this.Recipy.Ingredients
                    .All(recipyIngredient => UserService.Instance.UserVM.UserAccount.Ingredients
                        .Any(userIngredient => recipyIngredient.Id == userIngredient.Id &&
                             recipyIngredient.Amount <= userIngredient.Amount));
            } 
        }
        public List<IngredientViewModel> ConvertIngredientDtos()
        {
            List<IngredientViewModel> viewModels = new();
            foreach (IngredientDto dto in Recipy.Ingredients)
            {
                IngredientViewModel viewModel = new()
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        public static List<RecipyViewModel> ConvertRecipyDtos(List<RecipyDto> dtos)
        {
            List<RecipyViewModel> viewModels = new();
            foreach (RecipyDto dto in dtos)
            {
                RecipyViewModel viewModel = new()
                {
                    Recipy = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        public static async Task<List<RecipyViewModel>> GetAllRecipiesFromDB()
        {
            List<RecipyDto> dtos = await UserService.Instance.RecipyClient.GetAllAsync();

            return ConvertRecipyDtos(dtos);
        }
        public static List<RecipyViewModel> GetMakeableRecipies(List<RecipyViewModel> vms)
        {
            return vms.AsEnumerable()
                .Where(vm => vm.Recipy.Ingredients
                    .All(recipyIngredient => UserService.Instance.UserVM.UserAccount.Ingredients
                        .Any(userIngredient => recipyIngredient.Id == userIngredient.Id &&
                             recipyIngredient.Amount <= userIngredient.Amount)))
                .ToList();
        }
    }
}
