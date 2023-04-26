using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service.UoW.Interface;
using MyFridge_UserInterface_MAUI.ViewModel.Detail;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class GroceryViewModel : BindableObject
    {
        private readonly IUnitOfWork _uow;

        private ObservableCollection<DetailGroceryViewModel> groceryDetails;
        public ObservableCollection<DetailGroceryViewModel> GroceryDetails
        {
            get => groceryDetails;
            private set
            {
                groceryDetails = value;

                OnPropertyChanged(nameof(GroceryDetails));
            }
        }
        public GroceryViewModel(IUnitOfWork currentUserService)
        {
            _uow = currentUserService;

            groceryDetails = new();
        }
        public async Task RefreshIngredientsAsync()
        {
            IEnumerable<IngredientDto> ingredients = await _uow.IngredientClient.GetAllAsync();
            GroceryDetails = ToViewModel(ingredients.OrderBy(i => i.Name));
        }
        public void GetIngredientsFilteredLazy(string filter)
        {
            if (string.IsNullOrEmpty(filter)) 
                GroceryDetails = ToViewModel(_uow.IngredientClient.AllLazies.OrderBy(i => i.Name).ToList());
            else
                GroceryDetails = ToViewModel(_uow.IngredientClient.AllLazies
                    .Where(i => i.Name.ToLower().StartsWith(filter.ToLower()))
                    .OrderBy(i => i.Name).ToList());
        }
        public async Task AddGrocery(DetailGroceryViewModel ingredient, string amountResult)
        {
            bool parsed = uint.TryParse(amountResult, out uint amount);
            if (parsed)
            {
                IngredientAmountDto dto = new()
                {
                    Ingredient = ingredient.Ingredient,
                    Amount = amount
                };
                await _uow.UserClient.AddIngredientAmountAsync(dto, _uow.UserClient.Lazy.Id);
            }
        }
        public async Task NavigateBack()
        {
            await Shell.Current.GoToAsync($"..");
        }
        private ObservableCollection<DetailGroceryViewModel> ToViewModel(IEnumerable<IngredientDto> ingredients)
        {
            ObservableCollection<DetailGroceryViewModel> viewModels = new();
            foreach (IngredientDto dto in ingredients)
            {
                DetailGroceryViewModel viewModel = new()
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
