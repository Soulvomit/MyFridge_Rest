using MyFridge_Library_MAUI_Client.Interface;

namespace MyFridge_UserInterface_MAUI.Service.UoW.Interface
{
    public interface IUnitOfWork
    {
        public IAddressApiClient AddressClient { get; set; }
        public IAdminAccountApiClient AdminClient { get; set; }
        public IUserAccountApiClient UserClient { get; set; }
        public IIngredientApiClient IngredientClient { get; set; }
        public IIngredientAmountApiClient IngredientAmountClient { get; set; }
        public IGroceryApiClient GroceryClient { get; set; }
        public IOrderApiClient OrderClient { get; set; }
        public IRecipeApiClient RecipeClient { get; set; }
    }
}
