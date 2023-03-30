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

        public async Task<UserAccount> GetByEmailAsync(string email)
        {
            UserAccount? entityInDb = await dbSet
                .Where(user => user.Email == email)
                .FirstOrDefaultAsync();

            if (entityInDb == null) return null!;

            return entityInDb;
        }

        #region Address
        public async Task<bool> ChangeAddressAsync(int id, Address changeEntity)
        {
            Task<UserAccount?> t1 = GetAsync(id);
            Task<Address?> t2 = _context.Addresses
                .Where(address => address.Id == changeEntity.Id).FirstOrDefaultAsync();

            await Task.WhenAll(t1, t2);

            UserAccount? userEntityInDb = t1.Result;
            Address? addressEntityInDb = t2.Result;

            if (userEntityInDb == null) return false;

            if (addressEntityInDb == null)
                userEntityInDb.Address = changeEntity;
            else
                userEntityInDb.Address = addressEntityInDb;

            return true;
        }
        #endregion

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
        public async Task<bool> AddIngredientAsync(int id, IngredientAmount addEntity)
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
                IngredientAmount found = null;
                foreach (IngredientAmount ia in userEntityInDb.IngredientAmounts) 
                {
                    if(ia.IngredientId == addEntity.IngredientId)
                    {
                        found = ia;
                        break;
                    }
                }
                if (found != null) 
                    found.Amount += addEntity.Amount;
                else
                    userEntityInDb.IngredientAmounts.Add(ingredientAmountEntityInDb);
            }
            return true;
        }

        public async Task<bool> RemoveIngredientAsync(int id, int index)
        {
            UserAccount? entityInDb = await GetAsync(id);

            if (entityInDb == null) return false;

            IngredientAmount ingredientAmount = entityInDb.IngredientAmounts.ElementAt(index);
            entityInDb.IngredientAmounts.Remove(ingredientAmount);
            _context.IngredientAmounts.Remove(ingredientAmount);

            return true;
        }
        public async Task<bool> RemoveAmountAsync(int id, Ingredient updateEntity, float removeAmount, bool forceRemove = true)
        {
            UserAccount? userEntityInDb = await GetAsync(id);
            if (userEntityInDb == null) return false;

            foreach(IngredientAmount ia in userEntityInDb.IngredientAmounts)
            {
                if (ia.IngredientId == updateEntity.Id)
                {
                    if(ia.Amount - removeAmount > 0)
                        ia.Amount -= removeAmount;
                    else
                    {
                        if (forceRemove)
                            _context.IngredientAmounts.Remove(ia);
                        else
                        {
                            if (ia.Amount - removeAmount == 0)
                                _context.IngredientAmounts.Remove(ia);
                            else
                                break;
                        }
                    }
                    return true;
                }
            }
            return false;
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

        #region Recipies
        public async Task<List<Recipy>> GetValidRicipiesAsync(int id)
        {
            UserAccount? userEntityInDb = await GetAsync(id);
            if (userEntityInDb == null) return null;

            List<Recipy> makeable = _context.Recipies.AsEnumerable()
                .Where(r => r.IngredientAmounts
                    .All(ria => userEntityInDb.IngredientAmounts
                        .Any(uia => ria.IngredientId == uia.IngredientId && ria.Amount <= uia.Amount)))
                .ToList();

            return makeable;
        }
        public async Task<bool> MakeRicipiesAsync(int id, Recipy recipy, bool force = false)
        {
            UserAccount user = await GetAsync(id);
            if (user == null) return false;

            if (force)
            {
                Recipy? forcedRecipy = await _context.Recipies.FirstOrDefaultAsync(r => r.Id == recipy.Id);
                if (forcedRecipy == null) return false;

                var matchingIngredients = user.IngredientAmounts
                    .Join(forcedRecipy.IngredientAmounts,
                        userIngredient => userIngredient.IngredientId,
                        recipeIngredient => recipeIngredient.IngredientId,
                        (userIngredient, recipeIngredient) => (userIngredient, recipeIngredient));

                foreach (var (userIngredient, recipeIngredient) in matchingIngredients)
                {
                    await RemoveAmountAsync(id, recipeIngredient.Ingredient, recipeIngredient.Amount, force);
                }
                return true;
            }
            else
            {
                List<Recipy> validRecipies = await GetValidRicipiesAsync(id);
                Recipy? validRecipy = validRecipies.FirstOrDefault(r => r.Id == recipy.Id);
                if (validRecipy == null) return false;

                var matchingIngredients = user.IngredientAmounts
                    .Join(validRecipy.IngredientAmounts,
                        userIngredient => userIngredient.IngredientId,
                        recipeIngredient => recipeIngredient.IngredientId,
                        (userIngredient, recipeIngredient) => (userIngredient, recipeIngredient));

                foreach (var (userIngredient, recipeIngredient) in matchingIngredients)
                {
                    await RemoveAmountAsync(id, recipeIngredient.Ingredient, recipeIngredient.Amount, force);
                }
                return true;
            }
        }

        //public async Task<bool> MakeRicipiesAsync(int id, Recipy recipy, bool force = false)
        //{
        //    UserAccount? userEntityInDb = await GetAsync(id);
        //    if (userEntityInDb == null) return false;

        //    if (force)
        //    {
        //        foreach(IngredientAmount uia in userEntityInDb.IngredientAmounts)
        //        {
        //            foreach (IngredientAmount ria in recipy.IngredientAmounts) 
        //            {
        //                if(ria.IngredientId == uia.IngredientId)
        //                {
        //                    await RemoveAmountAsync(id, ria.Ingredient, ria.Amount, force);
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        List<Recipy> valid = await GetValidRicipiesAsync(id);
        //        Recipy temp = null;
        //        foreach(Recipy r in valid)
        //        {
        //            if(r.Id == recipy.Id)
        //            {
        //                temp = r;
        //                break;
        //            }
        //        }
        //        if(temp == null)
        //        {
        //            return false;
        //        }
        //        foreach (IngredientAmount uia in userEntityInDb.IngredientAmounts)
        //        {
        //            foreach (IngredientAmount ria in temp.IngredientAmounts)
        //            {
        //                if (ria.IngredientId == uia.IngredientId)
        //                {
        //                    await RemoveAmountAsync(id, ria.Ingredient, ria.Amount, force);
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //}
        #endregion

        #region Trash
        //public List<Recipy> ShowRicipiesAsync(int id)
        //{
        //    Task<UserAccount?> t1 = GetAsync(id);
        //    UserAccount? userEntityInDb = t1.Result;
        //    if (userEntityInDb == null) return null;

        //    List<Recipy> makeable = _context.Recipies.AsEnumerable()
        //        .Where(r => r.IngredientAmounts
        //            .All(ria => userEntityInDb.IngredientAmounts
        //                .Any(uia => ria.IngredientId == uia.IngredientId && ria.Amount <= uia.Amount)))
        //        .ToList();

        //    return makeable;


        //    //Task<UserAccount?> t1 = GetAsync(id);
        //    //UserAccount? userEntityInDb = t1.Result;

        //    //List<Recipy> all = _context.Recipies.ToList();
        //    //List<Recipy> makeable = new List<Recipy>();
        //    //foreach (Recipy recipy in all)
        //    //{
        //    //    bool match = true;
        //    //    foreach (IngredientAmount ria in recipy.IngredientAmounts)
        //    //    {
        //    //        match = false;
        //    //        foreach (IngredientAmount uia in userEntityInDb.IngredientAmounts)
        //    //        {
        //    //            if (ria.IngredientId == uia.IngredientId && ria.Amount < uia.Amount)
        //    //            {
        //    //                match = true;
        //    //                break;
        //    //            }
        //    //        }
        //    //        if (!match) break;
        //    //    }
        //    //    if (match) makeable.Add(recipy);
        //    //}

        //    //return makeable;
        //}
        #endregion
    }
}