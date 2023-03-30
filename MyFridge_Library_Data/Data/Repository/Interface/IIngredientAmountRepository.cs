using MyFridge_Library_Data.Data.Repository.Interface.Base;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository.Interface
{
    public interface IIngredientAmountRepository : IRepository<IngredientAmount>
    {
        public Task<bool> ChangeIngredientAsync(int id, Ingredient changeEntity);
    }
}
