using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class IngredientViewModel
    {
        public Color NameColor { get; set; } = Colors.White;
        public Color AmountColor { get; set; } = Colors.White;
        public IngredientDto Ingredient { get; set; }
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
    }
}
