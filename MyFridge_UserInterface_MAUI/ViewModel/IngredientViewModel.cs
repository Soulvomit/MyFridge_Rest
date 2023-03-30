using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class IngredientViewModel
    {
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
        public void SetColor(UserAccountViewModel vm)
        {
            bool userHas = false;
            bool userHasEnough = false;
            foreach (IngredientDto userIngredient in vm.UserAccount.Ingredients)
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
                this.NameColor = Colors.LightGreen;
                if (userHasEnough)
                    this.AmountColor = Colors.LightGreen;
                else
                {
                    this.NameColor = Colors.Yellow;
                    this.AmountColor = Colors.Red;
                }
            }
            else
            {
                this.NameColor = Colors.Red;
                this.AmountColor = Colors.Red;
            }
        }
        public static List<IngredientViewModel> ConvertIngredientDtos(List<IngredientDto> dtos)
        {
            List<IngredientViewModel> viewModels = new();
            foreach (IngredientDto dto in dtos)
            {
                IngredientViewModel viewModel = new()
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
