using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserViewModel
    {
        private readonly UserService _userService;
        public UserAccountDto User { get; private set; }

        public UserViewModel(UserService userService)
        {
            _userService = userService;
            User = _userService.User;
        }

        public List<IngredientViewModel> ConvertIngredientDtos()
        {
            List<IngredientViewModel> viewModels = new();
            foreach (IngredientDto dto in User.Ingredients)
            {
                IngredientViewModel viewModel = new(_userService)
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        public static List<UserViewModel> ConvertUserAccountDtos(List<UserAccountDto> dtos, UserService userService)
        {
            List<UserViewModel> viewModels = new();
            foreach (UserAccountDto dto in dtos)
            {
                UserViewModel viewModel = new(userService)
                {
                    User = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
