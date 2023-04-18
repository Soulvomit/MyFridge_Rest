using Microsoft.EntityFrameworkCore;
using MyFridge_Library_Data.Model;
using MyFridge_Library_Data.Model.Enum;

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
            //modelBuilder.Entity<Ingredient>()
            //    .HasKey(i => new { i.Name, i.Unit });
            //modelBuilder.Entity<Ingredient>().HasData(
            //    new Ingredient { Id = 1, Name = "Carrot", Unit = EUnit.Gram },
            //    new Ingredient { Id = 2, Name = "Ground Beef", Unit = EUnit.Gram },
            //    new Ingredient { Id = 3, Name = "Salt", Unit = EUnit.Gram },
            //    new Ingredient { Id = 4, Name = "Onion", Unit = EUnit.Gram },
            //    new Ingredient { Id = 5, Name = "Chicken Breast", Unit = EUnit.Gram},
            //    new Ingredient { Id = 6, Name = "Heavy Cream", Unit = EUnit.Milliliter },
            //    new Ingredient { Id = 7, Name = "Packet Popcorn", Unit = EUnit.Piece },
            //    new Ingredient { Id = 8, Name = "Butter", Unit = EUnit.Gram },
            //    new Ingredient { Id = 9, Name = "Water", Unit = EUnit.Milliliter },
            //    new Ingredient { Id = 10, Name = "Instant Coffee", Unit = EUnit.Gram },
            //    new Ingredient { Id = 11, Name = "Milk", Unit = EUnit.Milliliter }

            //    //add more ingredients as needed
                //);
  
        }
    }
}