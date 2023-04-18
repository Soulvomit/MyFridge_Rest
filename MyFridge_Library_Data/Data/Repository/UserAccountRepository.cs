using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.Data.Repository.Base;
using MyFridge_Library_Data.Data.Repository.Interface;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository
{
    public class UserAccountRepository : Repository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(ApplicationDbContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public override async Task<bool> UpdateAsync(UserAccount updateEntity)
        {
            if (updateEntity == null) return false;

            UserAccount? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Firstname = updateEntity.Firstname;
            entityInDb.Lastname = updateEntity.Lastname;
            entityInDb.Password = updateEntity.Password;
            entityInDb.PhoneNumber = updateEntity.PhoneNumber;
            entityInDb.Email = updateEntity.Email;
            entityInDb.BirthDate = updateEntity.BirthDate;

            return true;
        }

        public async Task<UserAccount?> GetByEmailAsync(string email)
        {
            UserAccount? entityInDb = await dbSet
                .Where(user => user.Email == email)
                .FirstOrDefaultAsync();

            if (entityInDb == null) return null;

            return entityInDb;
        }

        #region Ingredients
        public async Task<bool> AddIngredientAsync(int id, Ingredient addEntity, float addAmount)
        {
            Task<UserAccount?> t1 = GetAsync(id);
            Task<Ingredient?> t2 = _context.Ingredients
                .Where(ingredient => ingredient.Id == addEntity.Id)
                .FirstOrDefaultAsync();

            await Task.WhenAll(t1, t2);

            UserAccount? userEntityInDb = t1.Result;
            Ingredient? ingredientEntityInDb = t2.Result;

            if (userEntityInDb == null) return false;

            if (ingredientEntityInDb == null)
            {
                userEntityInDb.IngredientAmounts.Add(
                    new IngredientAmount
                    {
                        Ingredient = addEntity,
                        Amount = addAmount
                    });
            }
            else
            {
                IngredientAmount found = null;
                foreach (IngredientAmount ia in userEntityInDb.IngredientAmounts) 
                {
                    if(ia.IngredientId == ingredientEntityInDb.Id)
                    {
                        found = ia;
                        break;
                    }
                }
                if (found != null) 
                {
                    found.Amount += addAmount;
                }
                else
                {
                    userEntityInDb.IngredientAmounts.Add(
                        new IngredientAmount
                        {
                            Ingredient = ingredientEntityInDb,
                            Amount = addAmount
                        });
                }
            }
            return true;
        }
        public async Task<bool> AddIngredientAmountAsync(int id, IngredientAmount addEntity)
        {
            Task<UserAccount?> t1 = GetAsync(id);
            Task<IngredientAmount?> t2 = _context.IngredientAmounts
                .Where(ia => ia.Id == addEntity.Id)
                .FirstOrDefaultAsync();

            await Task.WhenAll(t1, t2);

            UserAccount? userEntityInDb = t1.Result;
            IngredientAmount? ingredientAmountEntityInDb = t2.Result;

            if (userEntityInDb == null) return false;

            if (ingredientAmountEntityInDb == null)
                userEntityInDb.IngredientAmounts.Add(addEntity);
            else
            {
                IngredientAmount? found = userEntityInDb.IngredientAmounts.
                                            FirstOrDefault(ia => ia.Id == addEntity.IngredientId); 
                if (found != null) 
                    found.Amount += addEntity.Amount;
                else
                    userEntityInDb.IngredientAmounts.Add(ingredientAmountEntityInDb);
            }
            return true;
        }

        public async Task<bool> RemoveIngredientAmountAsync(int id, int iaId, float removeAmount, bool forceRemove = true)
        {
            UserAccount? userEntityInDb = await GetAsync(id);
            if (userEntityInDb == null) return false;

            IngredientAmount? ia = userEntityInDb.IngredientAmounts.FirstOrDefault(ia => ia.Id == iaId);
            if (ia == null) return false;

            if (ia.Amount - removeAmount > 0)
            {
                ia.Amount -= removeAmount;
            }
            else
            {
                if (forceRemove)
                {
                    userEntityInDb.IngredientAmounts.Remove(ia);
                    _context.IngredientAmounts.Remove(ia);
                }
                else
                {
                    if (ia.Amount - removeAmount == 0)
                    {
                        userEntityInDb.IngredientAmounts.Remove(ia);
                        _context.IngredientAmounts.Remove(ia);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region Order
        public async Task<bool> AddOrderAsync(int id, Order addEntity)
        {
            Task<UserAccount?> t1 = GetAsync(id);
            Task<Order?> t2 = _context.Orders
                .Where(order => order.Id == addEntity.Id)
                .FirstOrDefaultAsync();

            await Task.WhenAll(t1, t2);

            UserAccount? userEntityInDb = t1.Result;
            Order? orderEntityInDb = t2.Result;

            if (userEntityInDb == null) return false;

            if (orderEntityInDb == null)
                userEntityInDb.Orders.Add(addEntity);
            else
                userEntityInDb.Orders.Add(orderEntityInDb);
            return true;
        }

        public async Task<bool> RemoveOrderAsync(int id, int index)
        {
            UserAccount? entityInDb = await GetAsync(id);

            if (entityInDb == null) return false;

            Order order = entityInDb.Orders.ElementAt(index);
            entityInDb.Orders.Remove(order);
            _context.Orders.Remove(order);

            return true;
        }
        #endregion
    }
}