using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.ComponentModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class IngredientDetailViewModel : INotifyPropertyChanged
    {
        private readonly CurrentUserService _cUserService;
        private readonly IngredientService _ingredientService;
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
        public Color NameColor { get; set; } = Colors.White;
        public Color AmountColor { get; set; } = Colors.White;
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
        public IngredientDetailViewModel(CurrentUserService cUserService,
            IngredientService ingredientService)
        {
            _cUserService = cUserService;
            _ingredientService = ingredientService;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
