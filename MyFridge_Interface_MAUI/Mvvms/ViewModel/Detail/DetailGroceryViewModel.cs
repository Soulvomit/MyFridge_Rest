using MyFridge_Library_Client_MAUI.ClientModel;
using MyFridge_Interface_MAUI.Mvvms.Service.Client.Interface;

namespace MyFridge_Interface_MAUI.Mvvms.ViewModel.Detail
{
    public class DetailGroceryViewModel : BindableObject
    {
        private readonly IClientService _clientService;

        #region Properties
        public IngredientDto Grocery { get; set; }
        public string Name
        {
            get => Grocery.Name;
            set
            {
                Grocery.Name = value;

                OnPropertyChanged(nameof(Name));
            }
        }
        public string UnitStr
        {
            get
            {
                if (Grocery.Unit == 2)
                    return "gram";
                if (Grocery.Unit == 1)
                    return "ml";
                else
                    return "pieces";
            }
        }
        #endregion

        public DetailGroceryViewModel(IClientService clientService)
        {
            _clientService = clientService;
        }
    }
}
