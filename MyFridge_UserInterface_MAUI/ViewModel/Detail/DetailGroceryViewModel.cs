using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_UserInterface_MAUI.ViewModel.Detail
{
    public class DetailGroceryViewModel : BindableObject
    {
        public IngredientDto Ingredient { get; set; }
        public string Name
        {
            get => Ingredient.Name;
            set
            {
                Ingredient.Name = value;

                OnPropertyChanged(nameof(Name));
            }
        }
        public string UnitStr
        {
            get
            {
                if (Ingredient.Unit == 2)
                    return "gram";
                if (Ingredient.Unit == 1)
                    return "ml";
                else
                    return "pieces";
            }
        }
    }
}
