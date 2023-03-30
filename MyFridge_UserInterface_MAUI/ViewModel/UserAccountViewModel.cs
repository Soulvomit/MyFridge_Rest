using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserAccountViewModel
    {
        public UserAccountDto UserAccount { get; set; }

        public List<IngredientViewModel> ConvertIngredientDtos()
        {
            List<IngredientViewModel> viewModels = new();
            foreach (IngredientDto dto in UserAccount.Ingredients)
            {
                IngredientViewModel viewModel = new()
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        public static List<UserAccountViewModel> ConvertUserAccountDtos(List<UserAccountDto> dtos)
        {
            List<UserAccountViewModel> viewModels = new();
            foreach (UserAccountDto dto in dtos)
            {
                UserAccountViewModel viewModel = new()
                {
                    UserAccount = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
