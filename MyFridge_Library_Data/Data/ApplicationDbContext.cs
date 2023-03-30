using Microsoft.EntityFrameworkCore;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipy> Recipies { get; set; }
        public DbSet<Grocery> Groceries { get; set; }
        public DbSet<IngredientAmount> IngredientAmounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<AdminAccount> Admins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>()
                .HasIndex(ua => ua.Email)
                .IsUnique();
        }
    }
}