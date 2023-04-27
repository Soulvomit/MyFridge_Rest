using MyFridge_Library_MAUI_Client.ClientRepository.Interface;

namespace MyFridge_UserInterface_MAUI.Mvvms.Service.Client.Interface
{
    public interface IClientService
    {
        public IAddressClientRepository AddressClient { get; set; }
        public IAdminAccountClientRepository AdminClient { get; set; }
        public IUserAccountClientRepository UserClient { get; set; }
        public IIngredientClientRepository IngredientClient { get; set; }
        public IIngredientAmountClientRepository IngredientAmountClient { get; set; }
        public IGroceryClientRepository GroceryClient { get; set; }
        public IOrderClientRepository OrderClient { get; set; }
        public IRecipeClientRepository RecipeClient { get; set; }
    }
}
