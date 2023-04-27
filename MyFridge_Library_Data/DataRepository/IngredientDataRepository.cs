﻿using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.DataContext;
using MyFridge_Library_Data.DataModel;
using MyFridge_Library_Data.DataRepository.Abstract;
using MyFridge_Library_Data.DataRepository.Interface;

namespace MyFridge_Library_Data.DataRepository
{
    public class IngredientDataRepository : DataRepository<Ingredient>, IIngredientDataRepository
    {
        public IngredientDataRepository(ApplicationDbContext context, ILogger log)
            : base(context, log)
        {
        }
        public override async Task<bool> UpdateAsync(Ingredient updateEntity)
        {
            if (updateEntity == null) return false;

            Ingredient? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Name = updateEntity.Name;
            entityInDb.Unit = updateEntity.Unit;

            return true;
        }
    }
}