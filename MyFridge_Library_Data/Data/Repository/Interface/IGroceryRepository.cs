using MyFridge_Library_Data.Data.Repository.Interface.Base;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository.Interface
{
    public interface IGroceryRepository : IRepository<Grocery>
    {
        public Task<bool> ChangeIngredientAsync(int id, Ingredient changeEntity, float changeAmount);
        public Task<bool> ChangeIngredientAsync(int id, IngredientAmount changeEntity);
    }
}
