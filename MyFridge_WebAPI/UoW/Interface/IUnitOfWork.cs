using MyFridge_Library_Data.Data.Repository.Interface;

namespace MyFridge_WebAPI.UoW.Interface
{
    public interface IUnitOfWork
    {
        public IAddressRepository Addresses { get; set; }
        public IAdminAccountRepository Admins { get; set; }
        public IUserAccountRepository Users { get; set; }
        public IIngredientRepository Ingredients { get; set; }
        public IIngredientAmountRepository IngredientAmounts { get; set; }
        public IGroceryRepository Groceries { get; set; }
        public IOrderRepository Orders { get; set; }
        public IRecipeRepository Recipes { get; set; }

        Task CompleteAsync();
    }
}
