using MyFridge_Library_Data.Data.Repository.Interface.Base;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository.Interface
{
    public interface IRecipyRepository : IRepository<Recipy>
   
    {
        public Task<bool> AddIngredientAsync(int id, Ingredient addEntity, float addAmount);
        public Task<bool> AddIngredientAsync(int id, IngredientAmount addEntity);
        public Task<bool> RemoveIngredientAsync(int id, int index);
    }
}