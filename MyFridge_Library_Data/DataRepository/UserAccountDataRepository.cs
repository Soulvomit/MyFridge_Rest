﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.DataContext;
using MyFridge_Library_Data.DataModel;
using MyFridge_Library_Data.DataRepository.Abstract;
using MyFridge_Library_Data.DataRepository.Interface;

namespace MyFridge_Library_Data.DataRepository
{
    public class UserAccountDataRepository : DataRepository<UserAccount>, IUserAccountDataRepository
    {
        public UserAccountDataRepository(ApplicationDbContext context, ILogger log)
            : base(context, log)
        {
        }

        public override async Task<bool> UpdateAsync(UserAccount updateEntity)
        {
            if (updateEntity == null) return false;

            UserAccount? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.FirstName = updateEntity.FirstName;
            entityInDb.LastName = updateEntity.LastName;
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
        public async Task<bool> AddIngredientAmountAsync(int id, IngredientAmount addEntity)
        {
            UserAccount? entityInDb = await GetAsync(id);

            if (entityInDb == null) return false;

            entityInDb.IngredientAmounts.Add(addEntity);
            return true;
        }
        public async Task<bool> BatchIngredientAmountAsync(int id, IngredientAmount addEntity)
        {
            UserAccount? entityInDb = await GetAsync(id);

            if (entityInDb == null) return false;

            IngredientAmount? foundEntity = entityInDb.IngredientAmounts.
                FirstOrDefault(ia => ia.Ingredient.Id == addEntity.Ingredient.Id);

            if (foundEntity != null)
                foundEntity.Amount += addEntity.Amount;
            else
                entityInDb.IngredientAmounts.Add(addEntity);

            return true;
        }
        public async Task<bool> RemoveAmountAsync(int id, int ingredientAmountId, float removeAmount, bool forceRemove = true)
        {
            UserAccount? entityInDb = await GetAsync(id);

            if (entityInDb == null) return false;

            IngredientAmount? foundEntity = entityInDb.IngredientAmounts
                .FirstOrDefault(ia => ia.Id == ingredientAmountId);

            if (foundEntity == null) return false;

            if (foundEntity.Amount >= removeAmount)
            {
                foundEntity.Amount -= removeAmount;

                if (foundEntity.Amount == 0)
                {
                    entityInDb.IngredientAmounts.Remove(foundEntity);
                    _context.IngredientAmounts.Remove(foundEntity);
                }
                return true;
            }
            if (foundEntity.Amount < removeAmount && forceRemove)
            {
                entityInDb.IngredientAmounts.Remove(foundEntity);
                _context.IngredientAmounts.Remove(foundEntity);

                return true;
            }
            return false;
        }
        public async Task<bool> RemoveIngredientAmountAsync(int id, IngredientAmount removeEntity)
        {
            UserAccount? entityInDb = await GetAsync(id);

            if (entityInDb == null) return false;

            entityInDb.IngredientAmounts.Remove(removeEntity);
            _context.IngredientAmounts.Remove(removeEntity);

            return true;
        }
        public async Task<bool> RemoveIngredientAmountAsync(int id, int ingredientAmountId)
        {
            UserAccount? entityInDb = await GetAsync(id);
            if (entityInDb == null) return false;

            IngredientAmount? iaEntityInDb = await _context.IngredientAmounts.FindAsync(ingredientAmountId);
            if (iaEntityInDb == null) return false;

            entityInDb.IngredientAmounts.Remove(iaEntityInDb);
            _context.IngredientAmounts.Remove(iaEntityInDb);

            return true;
        }
        //public async Task<bool> AddIngredientAsync(int id, Ingredient addEntity, float addAmount)
        //{
        //    Task<UserAccount?> t1 = GetAsync(id);
        //    Task<Ingredient?> t2 = _context.Ingredients
        //        .Where(ingredient => ingredient.Id == addEntity.Id)
        //        .FirstOrDefaultAsync();

        //    await Task.WhenAll(t1, t2);

        //    UserAccount? userEntityInDb = t1.Result;
        //    Ingredient? ingredientEntityInDb = t2.Result;

        //    if (userEntityInDb == null) return false;

        //    if (ingredientEntityInDb == null)
        //    {
        //        userEntityInDb.IngredientAmounts.Add(
        //            new IngredientAmount
        //            {
        //                Ingredient = addEntity,
        //                Amount = addAmount
        //            });
        //    }
        //    else
        //    {
        //        IngredientAmount? found = null;
        //        foreach (IngredientAmount ia in userEntityInDb.IngredientAmounts) 
        //        {
        //            if(ia.IngredientId == ingredientEntityInDb.Id)
        //            {
        //                found = ia;
        //                break;
        //            }
        //        }
        //        if (found != null) 
        //        {
        //            found.Amount += addAmount;
        //        }
        //        else
        //        {
        //            userEntityInDb.IngredientAmounts.Add(
        //                new IngredientAmount
        //                {
        //                    Ingredient = ingredientEntityInDb,
        //                    Amount = addAmount
        //                });
        //        }
        //    }
        //    return true;
        //}
        //public async Task<bool> AddIngredientAmountAsync(int id, IngredientAmount addEntity)
        //{
        //    Task<UserAccount?> t1 = GetAsync(id);
        //    Task<IngredientAmount?> t2 = _context.IngredientAmounts
        //        .FirstOrDefaultAsync(ia => ia.Id == addEntity.Id);

        //    await Task.WhenAll(t1, t2);

        //    UserAccount? userEntityInDb = t1.Result;
        //    IngredientAmount? ingredientAmountEntityInDb = t2.Result;

        //    if (userEntityInDb == null) return false;

        //    if (ingredientAmountEntityInDb == null)
        //    {
        //        IngredientAmount? found = userEntityInDb.IngredientAmounts.
        //            FirstOrDefault(ia => ia.Ingredient.Id == addEntity.Ingredient.Id);
        //        if (found != null)
        //            found.Amount += addEntity.Amount;
        //        else
        //            userEntityInDb.IngredientAmounts.Add(addEntity);
        //    }
        //    else
        //    {
        //        IngredientAmount? found = userEntityInDb.IngredientAmounts.
        //            FirstOrDefault(ia => ia.IngredientId == addEntity.IngredientId);
        //        if (found != null)
        //            found.Amount += addEntity.Amount;
        //        else
        //            userEntityInDb.IngredientAmounts.Add(ingredientAmountEntityInDb);
        //    }
        //    return true;
        //}
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

        public async Task<bool> RemoveOrderAsync(int id, Order removeEntity)
        {
            UserAccount? userEntityInDb = await GetAsync(id);

            if (userEntityInDb == null) return false;

            return userEntityInDb.Orders.Remove(removeEntity);
        }
        #endregion
    }
}