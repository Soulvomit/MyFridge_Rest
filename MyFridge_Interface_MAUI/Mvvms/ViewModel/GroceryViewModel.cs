using MyFridge_Library_Client_MAUI.ClientModel;
using MyFridge_Interface_MAUI.Mvvms.Service.Client.Interface;
using MyFridge_Interface_MAUI.Mvvms.ViewModel.Detail;
using System.Collections.ObjectModel;

namespace MyFridge_Interface_MAUI.Mvvms.ViewModel
{
    public class GroceryViewModel : BindableObject
    {
        #region Privates
        private readonly IClientService _clientService;
        private ObservableCollection<DetailGroceryViewModel> groceryDetails;
        #endregion

        #region Properties
        public ObservableCollection<DetailGroceryViewModel> GroceryDetails
        {
            get => groceryDetails;
            private set
            {
                groceryDetails = value;

                OnPropertyChanged(nameof(GroceryDetails));
            }
        }
        #endregion

        public GroceryViewModel(IClientService clientService)
        {
            _clientService = clientService;

            groceryDetails = new();
        }
        public async Task RefreshAsync()
        {
            IEnumerable<IngredientDto> groceries = await _clientService.IngredientClient.GetAllAsync();
            GroceryDetails = ToViewModel(groceries.OrderBy(i => i.Name));
        }
        public void GetFilteredLazy(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                GroceryDetails = ToViewModel(_clientService.IngredientClient.AllLazies.OrderBy(i => i.Name).ToList());
            else
                GroceryDetails = ToViewModel(_clientService.IngredientClient.AllLazies
                    .Where(i => i.Name.ToLower().StartsWith(filter.ToLower()))
                    .OrderBy(i => i.Name).ToList());
        }
        public async Task Add(DetailGroceryViewModel ingredient, string amountResult)
        {
            bool parsed = uint.TryParse(amountResult, out uint amount);
            if (parsed)
            {
                IngredientAmountDto dto = new()
                {
                    Ingredient = ingredient.Grocery,
                    Amount = amount
                };
                await _clientService.UserClient.AddIngredientAmountAsync(dto, _clientService.UserClient.Lazy.Id);
            }
        }
        private ObservableCollection<DetailGroceryViewModel> ToViewModel(IEnumerable<IngredientDto> ingredients)
        {
            ObservableCollection<DetailGroceryViewModel> viewModels = new();
            foreach (IngredientDto dto in ingredients)
            {
                DetailGroceryViewModel viewModel = new(_clientService)
                {
                    Grocery = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
