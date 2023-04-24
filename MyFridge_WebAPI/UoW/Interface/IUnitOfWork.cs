using MyFridge_Library_Data.Data.Repository.Interface;

namespace MyFridge_WebAPI.UoW.Interface
{
    public interface IUnitOfWork
    {
        IAddressRepository Addresses { get; set; }
        IAdminAccountRepository Admins { get; set; }
        IUserAccountRepository Users { get; set; }
        IIngredientRepository Ingredients { get; set; }
        IIngredientAmountRepository IngredientAmounts { get; set; }
        IGroceryRepository Groceries { get; set; }
        IOrderRepository Orders { get; set; }
        IRecipeRepository Recipes { get; set; }

        Task CompleteAsync();
    }
}
