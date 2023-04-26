using MyFridge_Library_MAUI_Client;
using MyFridge_Library_MAUI_Client.Interface;
using MyFridge_UserInterface_MAUI.Service.UoW.Interface;
using System;

namespace MyFridge_UserInterface_MAUI.Service.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
    {
        private readonly string baseAddress = "https://localhost:44364/";
        public IAddressApiClient AddressClient { get; set; }
        public IAdminAccountApiClient AdminClient { get; set; }
        public IUserAccountApiClient UserClient { get; set; }
        public IIngredientApiClient IngredientClient { get; set; }
        public IIngredientAmountApiClient IngredientAmountClient { get; set; }
        public IGroceryApiClient GroceryClient { get; set; }
        public IOrderApiClient OrderClient { get; set; }
        public IRecipeApiClient RecipeClient { get; set; }
        public UnitOfWork()
        {
            AddressClient = new AddressApiClient(baseAddress);
            AdminClient = new AdminAccountApiClient(baseAddress);
            UserClient = new UserAccountApiClient(baseAddress);
            IngredientClient = new IngredientApiClient(baseAddress);
            IngredientAmountClient = new IngredientAmountApiClient(baseAddress);
            GroceryClient = new GroceryApiClient(baseAddress);
            OrderClient = new OrderApiClient(baseAddress);
            RecipeClient = new RecipeApiClient(baseAddress);
        }

        public void Dispose()
        {
            
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
