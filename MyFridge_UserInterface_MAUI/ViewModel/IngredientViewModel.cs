using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class IngredientViewModel
    {
        private readonly UserService _userService;
        public Color NameColor { get; set; } = Colors.White;
        public Color AmountColor { get; set; } = Colors.White;
        public IngredientDto Ingredient { get; set; }
        public string ExpirationDateStr
        {
            get
            {
                if (Ingredient.ExpirationDate == null)
                    return null;

                return ((DateTime)Ingredient.ExpirationDate).ToShortDateString();
            }
        }
        public string UnitStr
        {
            get
            {
                if (Ingredient.Unit == 0)
                    if (Ingredient.Amount < 2)
                        return "piece";
                    else
                        return "pieces";
                if (Ingredient.Unit == 1)
                    return "ml";
                else
                    if (Ingredient.Amount < 2)
                        return "gram";
                else
                    return "grams";
            }
        }

        public IngredientViewModel(UserService userService)
        {
            _userService = userService;
        }

        public void SetColor()
        {
            bool userHas = false;
            bool userHasEnough = false;
            foreach (IngredientDto userIngredient in _userService.User.Ingredients)
            {
                if (userIngredient.Id == this.Ingredient.Id)
                {
                    userHas = true;
                    if (userIngredient.Amount >= this.Ingredient.Amount)
                    {
                        userHasEnough = true;
                    }
                    break;
                }
            }
            if (userHas)
            {
                this.NameColor = Color.FromArgb("#4CAF50");
                if (userHasEnough)
                    this.AmountColor = Color.FromArgb("#4CAF50");
                else
                {
                    this.NameColor = Color.FromArgb("#FFC107");
                    this.AmountColor = Color.FromArgb("#FF6B6B");
                }
            }
            else
            {
                this.NameColor = Color.FromArgb("#FF6B6B");
                this.AmountColor = Color.FromArgb("#FF6B6B");
            }
        }
        public static List<IngredientViewModel> ConvertIngredientDtos(List<IngredientDto> dtos, UserService userService)
        {
            List<IngredientViewModel> viewModels = new();
            foreach (IngredientDto dto in dtos)
            {
                IngredientViewModel viewModel = new(userService)
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
