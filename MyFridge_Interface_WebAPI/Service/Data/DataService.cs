using MyFridge_Library_Data.DataContext;
using MyFridge_Library_Data.DataRepository;
using MyFridge_Library_Data.DataRepository.Interface;
using MyFridge_Interface_WebAPI.Service.Data.Interface;

namespace MyFridge_Interface_WebAPI.Service.Data
{
    public sealed class DataService : IDataService, IAsyncDisposable, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _log;

        public IAddressDataRepository Addresses { get; set; }
        public IAdminAccountDataRepository Admins { get; set; }
        public IUserAccountDataRepository Users { get; set; }
        public IIngredientDataRepository Ingredients { get; set; }
        public IIngredientAmountDataRepository IngredientAmounts { get; set; }
        public IGroceryDataRepository Groceries { get; set; }
        public IOrderDataRepository Orders { get; set; }
        public IRecipeDataRepository Recipes { get; set; }
        public DataService(ApplicationDbContext context, ILoggerFactory logFactory)
        {
            _context = context;
            _log = logFactory.CreateLogger("logs");

            Addresses = new AddressDataRepository(_context, _log);
            Admins = new AdminAccountDataRepository(_context, _log);
            Users = new UserAccountDataRepository(_context, _log);
            Ingredients = new IngredientDataRepository(_context, _log);
            IngredientAmounts = new IngredientAmountDataRepository(_context, _log);
            Groceries = new GroceryDataRepository(_context, _log);
            Orders = new OrderDataRepository(_context, _log);
            Recipes = new RecipeDataRepository(_context, _log);

            ApplicationDbContextSeeder.Seed(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _log.Log(LogLevel.None, "disposing of context...");
            _context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            _log.Log(LogLevel.None, "disposing of context...");
            await _context.DisposeAsync();
        }
    }
}
