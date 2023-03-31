using MyFridge_Library_Data.Model;
using MyFridge_Library_Data.Model.Enum;

namespace MyFridge_Library_Data.Data
{
    public static class ApplicationDbContextSeeder
    {
        private static bool seeded = false;
        public static void Seed(ApplicationDbContext context, bool resetSeeder = false)
        {
            if (resetSeeder) seeded = false;
            if (!seeded)
            {
                GrocerySeed(context);
                OrderSeed(context);
                RecipySeed(context);
                UserAccountSeed(context);
                context.SaveChanges();
                seeded = true;
            }
        }

        #region GrocerySeed
        public static void GrocerySeed(ApplicationDbContext context)
        {
            List<Grocery> tempGroceries = new List<Grocery>()
            {
                new Grocery()
                {
                    Brand = "VeggiesCo",
                    SalePrice = 12,
                    IngredientAmount = new IngredientAmount
                    {
                        Amount = 750,
                        Ingredient = new Ingredient
                        {
                            Name = "Onion",
                            Unit = EUnit.Gram 
                        }
                    }
                },
                new Grocery()
                {
                    Brand = "Meat N Greet",
                    SalePrice = 40,
                    IngredientAmount = new IngredientAmount
                    {
                        Amount = 500,
                        Ingredient = new Ingredient
                        {
                            Name = "Chicken Breast",
                            Unit = EUnit.Gram
                        }
                    }
                },
                new Grocery()
                {
                    Brand = "Arla",
                    SalePrice = 7,
                    IngredientAmount = new IngredientAmount
                    {
                        Amount = 1000,
                        Ingredient = new Ingredient
                        {
                            Name = "Cream",
                            Unit = EUnit.Milliliter
                        }
                    }
                }
            };
            context.Groceries.AddRange(tempGroceries);
        }
        #endregion

        #region OrderSeed
        private static void OrderSeed(ApplicationDbContext context)
        {
            List<Order> tempOrders = new List<Order>()
            {

            };
            context.Orders.AddRange(tempOrders);
        }
        #endregion

        #region RecipySeed
        public static void RecipySeed(ApplicationDbContext context)
        {
            List<Recipy> tempRecipies = new List<Recipy>()
            {
                new Recipy()
                {
                    Name = "Buttered Popcorn",
                    Description =   """
                                    1: Cook popcorn in microwave for 3 mins. 
                                    2: Add butter to popcorn and wait until melted. 
                                    3: Done.
                                    """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() 
                        { 
                            Ingredient = new Ingredient 
                            { 
                                Name = "PacketPopcorn", 
                                Unit = EUnit.Piece 
                            }, 
                            Amount = 1 
                        },
                        new IngredientAmount() 
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Butter",
                                Unit = EUnit.Gram
                            },
                            Amount = 100
                        }
                    },
                },
                new Recipy()
                {
                    Name = "Beef N Carrot", Description =   """
                                                            1: Cook beef on pan for 20 mins. 
                                                            2: Add sliced carrot and cook for 10 mins further. 
                                                            3: Add salt. 
                                                            4: Done.
                                                            """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() 
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Carrot",
                                Unit = EUnit.Gram 
                            }, 
                            Amount = 200
                        },
                        new IngredientAmount() 
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Ground Beef",
                                Unit = EUnit.Gram 
                            },
                            Amount = 300
                        },
                        new IngredientAmount()
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Salt",
                                Unit = EUnit.Gram
                            },
                            Amount = 5
                        }
                    }
                },
                new Recipy()
                {
                    Name = "Cafe Latte",
                    Description =   """
                                    1: Boil Water. 
                                    2: Add instant coffee. 
                                    3: Whip milk. 
                                    4: Add milk. 
                                    5: Done.
                                    """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount()
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Water",
                                Unit = EUnit.Milliliter
                            },
                            Amount = 50
                        },
                        new IngredientAmount()
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Instant Coffe",
                                Unit = EUnit.Gram
                            },
                            Amount = 10
                        },
                        new IngredientAmount()
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Unit = EUnit.Milliliter
                            },
                            Amount = 70
                        }
                    }
                }
            };
            context.Recipies.AddRange(tempRecipies);
        }
        #endregion

        #region UserAccountSeed
        public static void UserAccountSeed(ApplicationDbContext context)
        {
            List<UserAccount> tempUsers = new List<UserAccount>()
            {
                new UserAccount
                {
                    Firstname = "Jonas",
                    Lastname = "Brown",
                    Password = "password",
                    Address = new Address()
                    {
                        Id = 1,
                        Street = "Cambridge Street 231",
                        Extension = null,
                        City = "New York",
                        State = "New York",
                        ZipCode = "12345-12345",
                        Country = ECountry.UnitedStates
                    },
                    Email = "email@email.com",
                    PhoneNumber = 4540302010,
                    BirthDate = new DateTime(1983, 02, 09, 00, 00, 00),
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount()
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Apple",
                                Unit = EUnit.Piece
                            },
                            Amount = 10
                        },
                        new IngredientAmount()
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Banana",
                                Unit = EUnit.Piece
                            },
                            Amount = 3
                        },
                        new IngredientAmount()
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Flour",
                                Unit = EUnit.Gram
                            },
                            Amount = 1000
                        },
                        new IngredientAmount()
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Kefir",
                                Unit = EUnit.Milliliter
                            },
                            Amount = 1000
                        }
                    }
                },
                new UserAccount
                {
                    Firstname = "Bertram Vandsted",
                    Lastname = "Brown",
                    Password = "password1",
                    Address = new Address()
                    {
                        Id = 2,
                        Street = "Skellet 1",
                        Extension = "2. MF.",
                        City = "Glostup",
                        State = "Copenhagen",
                        ZipCode = "2600",
                        Country = ECountry.Denmark
                    },
                    Email = "email1@email.com",
                    PhoneNumber = 4540302010,
                    BirthDate = new DateTime(2022, 01, 29, 00, 00, 00)
                },
                new UserAccount
                {
                    Firstname = "Ada-Noelle Kirsten Vandsted",
                    Lastname = "Brown",
                    Password = "password2",
                    Address = new Address()
                    {
                        Id = 3,
                        Street = "Holmvej 259",
                        Extension = "1. MF.",
                        City = "Hoejbjerg",
                        State = "Aarhus",
                        ZipCode = "8270",
                        Country = ECountry.Denmark
                    },
                    Email = "email2@email.com",
                    PhoneNumber = 4540302010,
                    BirthDate = new DateTime(2022, 01, 29, 00, 00, 00)
                }
            };
            context.Users.AddRange(tempUsers);
        }
        #endregion
    }
}
