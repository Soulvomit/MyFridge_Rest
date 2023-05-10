using Data_Library.Repository.Interface;

namespace Data_Interface.Service.Data.Interface
{
    public interface IDataService
    {
        public IAddressDataRepository Addresses { get; set; }
        public IAdminAccountDataRepository Admins { get; set; }
        public IUserAccountDataRepository Users { get; set; }
        public IIngredientDataRepository Ingredients { get; set; }
        public IIngredientAmountDataRepository IngredientAmounts { get; set; }
        public IGroceryDataRepository Groceries { get; set; }
        public IOrderDataRepository Orders { get; set; }
        public IRecipeDataRepository Recipes { get; set; }

        Task CompleteAsync();
    }
}
