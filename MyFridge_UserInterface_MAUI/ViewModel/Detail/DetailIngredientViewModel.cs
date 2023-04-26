using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service.UoW.Interface;

namespace MyFridge_UserInterface_MAUI.ViewModel.Detail
{
    public class DetailIngredientViewModel : BindableObject
    {
        private readonly IUnitOfWork _uow;
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
        public bool CurrentUserHas
        {
            get
            {
                UserAccountDto user = _uow.UserClient.Lazy;
                IngredientAmountDto test = user.Ingredients
                    .FirstOrDefault(i => i.Ingredient.Id == IngredientAmount.Ingredient.Id);
                return test != null;
            }
        }
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
        public Color NameColor { get; set; } = Colors.White;
        public Color AmountColor { get; set; } = Colors.White;
        public DetailIngredientViewModel(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task RefreshAndSaveAsync()
        {
            IngredientAmount = await _uow.IngredientAmountClient.UpsertAsync(IngredientAmount);
        }
        public void UpdateColorsAsync()
        {
            UserAccountDto user = _uow.UserClient.Lazy;

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
        public async Task NavigateBack()
        {
            await Shell.Current.GoToAsync($"..");
        }
    }
}
