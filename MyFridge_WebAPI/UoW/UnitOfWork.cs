﻿using MyFridge_Library_Data.Data;
using MyFridge_Library_Data.Data.Repository;
using MyFridge_Library_Data.Data.Repository.Interface;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.UoW
{
    public sealed class UnitOfWork : IUnitOfWork, IAsyncDisposable, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public IAddressRepository Addresses { get; set; }
        public IAdminAccountRepository Admins { get; set; }
        public IUserAccountRepository Users { get; set; }
        public IIngredientRepository Ingredients { get; set; }
        public IIngredientAmountRepository IngredientAmounts { get; set; }
        public IGroceryRepository Groceries { get; set; }
        public IOrderRepository Orders { get; set; }
        public IRecipeRepository Recipes { get; set; }
        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Addresses = new AddressRepository(_context, _logger);
            Admins = new AdminAccountRepository(_context, _logger);
            Users = new UserAccountRepository(_context, _logger);
            Ingredients = new IngredientRepository(_context, _logger);
            IngredientAmounts = new IngredientAmountRepository(_context, _logger);
            Groceries = new GroceryRepository(_context, _logger);
            Orders = new OrderRepository(_context, _logger);
            Recipes = new RecipeRepository(_context, _logger);

            ApplicationDbContextSeeder.Seed(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _logger.Log(LogLevel.None, "disposing of context...");
            _context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            _logger.Log(LogLevel.None, "disposing of context...");
            await _context.DisposeAsync();
        }
    }
}
