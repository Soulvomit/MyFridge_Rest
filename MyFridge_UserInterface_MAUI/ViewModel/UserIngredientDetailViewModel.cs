using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.ComponentModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserIngredientDetailViewModel : INotifyPropertyChanged
    {
        private readonly CurrentUserService _cUserService;
        private readonly IngredientAmountService _iaService;
        public IngredientDto Ingredient { get; set; }
        public DateTime? ExpirationDate
        {
            get => Ingredient.ExpirationDate;
            set
            {
                Ingredient.ExpirationDate = value;

                OnPropertyChanged(nameof(ExpirationDate));
            }
        }
        public float Amount
        {
            get => Ingredient.Amount;
            set
            {
                Ingredient.Amount = value;

                OnPropertyChanged(nameof(Amount));
            }
        }
        public Color NameColor { get; set; } = Colors.White;
        public Color AmountColor { get; set; } = Colors.White;
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
                if (Ingredient.Unit == 2)
                    return "gram";
                if (Ingredient.Unit == 1)
                    return "ml";
                else
                    if (Ingredient.Amount < 2)
                    return "piece";
                else
                    return "pieces";
            }
        }
        public UserIngredientDetailViewModel(CurrentUserService cUserService,
            IngredientAmountService iaService)
        {
            _cUserService = cUserService;
            _iaService = iaService;


        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task Upsert()
        {
            Ingredient = await _iaService.Client.UpsertAsync(Ingredient);
        }
        public async Task SetColor()
        {
            bool userHas = false;
            bool userHasEnough = false;
            UserAccountDto user = await _cUserService.GetUserLazyAsync();

            foreach (IngredientDto userIngredient in user.Ingredients)
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
    }
}
