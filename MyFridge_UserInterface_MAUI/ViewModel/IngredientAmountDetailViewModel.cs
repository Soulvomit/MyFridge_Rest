using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class IngredientAmountDetailViewModel : BindableObject
    {
        private readonly CurrentUserService _cUserService;
        private readonly IngredientAmountService _iaService;
        public IngredientAmountDto IngredientAmount { get; set; }

        public string Name
        {
            get => IngredientAmount.Ingredient.Name;
            set
            {
                IngredientAmount.Ingredient.Name = value;

                OnPropertyChanged(nameof(Name));
            }
        }
        public float Amount
        {
            get => IngredientAmount.Amount;
            set
            {
                IngredientAmount.Amount = value;

                OnPropertyChanged(nameof(Amount));
            }
        }
        public DateTime? ExpirationDate
        {
            get => IngredientAmount.ExpirationDate;
            set
            {
                IngredientAmount.ExpirationDate = value;

                OnPropertyChanged(nameof(ExpirationDate));
            }
        }
        public Color NameColor { get; set; } = Colors.White;
        public Color AmountColor { get; set; } = Colors.White;
        public string ExpirationDateStr
        {
            get
            {
                if (IngredientAmount.ExpirationDate == null)
                    return null;

                return ((DateTime)IngredientAmount.ExpirationDate).ToShortDateString();
            }
        }
        public string UnitStr
        {
            get
            {
                if (IngredientAmount.Ingredient.Unit == 2)
                    return "gram";
                if (IngredientAmount.Ingredient.Unit == 1)
                    return "ml";
                else
                    if (IngredientAmount.Amount < 2)
                        return "piece";
                else
                    return "pieces";
            }
        }
        public IngredientAmountDetailViewModel(CurrentUserService cUserService,
            IngredientAmountService iaService)
        {
            _cUserService = cUserService;
            _iaService = iaService;


        }
        public async Task SaveAsync()
        {
            IngredientAmount = await _iaService.Client.UpsertAsync(IngredientAmount);
        }
        public async Task UpdateColorAsync()
        {
            UserAccountDto user = await _cUserService.GetUserLazyAsync();

            IngredientAmountDto userIngredientAmount = user.Ingredients
                .FirstOrDefault(ui => ui.Ingredient.Id == IngredientAmount.Ingredient.Id);

            NameColor = Color.FromArgb("#FF6B6B");
            AmountColor = Color.FromArgb("#FF6B6B");

            if (userIngredientAmount != null)
            {
                NameColor = Color.FromArgb("#4CAF50");

                if (userIngredientAmount.Amount >= IngredientAmount.Amount)
                    AmountColor = Color.FromArgb("#4CAF50");
            }
        }
    }
}
