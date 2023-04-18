using Microsoft.EntityFrameworkCore;
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
                IngredientSeed(context);
                GrocerySeed(context);
                OrderSeed(context);
                RecipySeed(context);
                UserAccountSeed(context);
                context.SaveChanges();
                seeded = true;
            }
        }

        #region
        public static void IngredientSeed(ApplicationDbContext context)
        {
            context.Ingredients.AddRange(
                new Ingredient { Id = 1, Name = "Carrot", Unit = EUnit.Gram },
                new Ingredient { Id = 2, Name = "Ground Beef", Unit = EUnit.Gram },
                new Ingredient { Id = 3, Name = "Salt", Unit = EUnit.Gram },
                new Ingredient { Id = 4, Name = "Onion", Unit = EUnit.Gram },
                new Ingredient { Id = 5, Name = "Chicken Breast", Unit = EUnit.Gram },
                new Ingredient { Id = 6, Name = "Heavy Cream", Unit = EUnit.Milliliter },
                new Ingredient { Id = 7, Name = "Packet Popcorn", Unit = EUnit.Piece },
                new Ingredient { Id = 8, Name = "Butter", Unit = EUnit.Gram },
                new Ingredient { Id = 9, Name = "Water", Unit = EUnit.Milliliter },
                new Ingredient { Id = 10, Name = "Instant Coffee", Unit = EUnit.Gram },
                new Ingredient { Id = 11, Name = "Milk", Unit = EUnit.Milliliter },
                new Ingredient { Id = 12, Name = "Apple", Unit = EUnit.Piece },
                new Ingredient { Id = 13, Name = "Banana", Unit = EUnit.Piece },
                new Ingredient { Id = 14, Name = "Wheat Flour", Unit = EUnit.Gram },
                new Ingredient { Id = 15, Name = "Kefir", Unit = EUnit.Milliliter },
                new Ingredient { Id = 16, Name = "Sugar", Unit = EUnit.Gram },
                new Ingredient { Id = 17, Name = "Brown Sugar", Unit = EUnit.Gram },
                new Ingredient { Id = 18, Name = "Egg", Unit = EUnit.Piece },
                new Ingredient { Id = 19, Name = "Baking Powder", Unit = EUnit.Gram },
                new Ingredient { Id = 20, Name = "Baking Soda", Unit = EUnit.Gram },
                new Ingredient { Id = 21, Name = "Olive Oil", Unit = EUnit.Milliliter },
                new Ingredient { Id = 22, Name = "Vegetable Oil", Unit = EUnit.Milliliter },
                new Ingredient { Id = 23, Name = "Canola Oil", Unit = EUnit.Milliliter },
                new Ingredient { Id = 24, Name = "Rice", Unit = EUnit.Gram },
                new Ingredient { Id = 25, Name = "Pasta", Unit = EUnit.Gram },
                new Ingredient { Id = 26, Name = "Bread", Unit = EUnit.Piece },
                new Ingredient { Id = 27, Name = "Garlic", Unit = EUnit.Gram },
                new Ingredient { Id = 28, Name = "Ginger", Unit = EUnit.Gram },
                new Ingredient { Id = 29, Name = "Black Pepper", Unit = EUnit.Gram },
                new Ingredient { Id = 30, Name = "Cumin", Unit = EUnit.Gram },
                new Ingredient { Id = 31, Name = "Paprika", Unit = EUnit.Gram },
                new Ingredient { Id = 32, Name = "Cayenne Pepper", Unit = EUnit.Gram },
                new Ingredient { Id = 33, Name = "Chili Powder", Unit = EUnit.Piece },
                new Ingredient { Id = 34, Name = "Red Pepper Flakes", Unit = EUnit.Gram },
                new Ingredient { Id = 35, Name = "Oregano", Unit = EUnit.Gram },
                new Ingredient { Id = 36, Name = "Basil", Unit = EUnit.Gram },
                new Ingredient { Id = 37, Name = "Parsley", Unit = EUnit.Gram },
                new Ingredient { Id = 38, Name = "Thyme", Unit = EUnit.Gram },
                new Ingredient { Id = 39, Name = "Rosemary", Unit = EUnit.Gram },
                new Ingredient { Id = 40, Name = "Lemon", Unit = EUnit.Piece },
                new Ingredient { Id = 41, Name = "Lime", Unit = EUnit.Piece },
                new Ingredient { Id = 42, Name = "Orange", Unit = EUnit.Piece },
                new Ingredient { Id = 43, Name = "Soy Sauce", Unit = EUnit.Milliliter },
                new Ingredient { Id = 44, Name = "Worcestershire Sauce", Unit = EUnit.Milliliter },
                new Ingredient { Id = 45, Name = "Honey", Unit = EUnit.Gram },
                new Ingredient { Id = 46, Name = "Maple Syrup", Unit = EUnit.Milliliter },
                new Ingredient { Id = 47, Name = "Tomato", Unit = EUnit.Piece },
                new Ingredient { Id = 48, Name = "Potato", Unit = EUnit.Piece },
                new Ingredient { Id = 49, Name = "Sweet Potato", Unit = EUnit.Piece },
                new Ingredient { Id = 50, Name = "Peas", Unit = EUnit.Gram },
                new Ingredient { Id = 51, Name = "Corn", Unit = EUnit.Gram },
                new Ingredient { Id = 52, Name = "Bell Pepper", Unit = EUnit.Piece },
                new Ingredient { Id = 53, Name = "Broccoli", Unit = EUnit.Gram },
                new Ingredient { Id = 54, Name = "Cauliflower", Unit = EUnit.Gram },
                new Ingredient { Id = 55, Name = "Spinach", Unit = EUnit.Gram },
                new Ingredient { Id = 56, Name = "Kale", Unit = EUnit.Gram },
                new Ingredient { Id = 57, Name = "Mushroom", Unit = EUnit.Gram },
                new Ingredient { Id = 58, Name = "Cabbage", Unit = EUnit.Gram },
                new Ingredient { Id = 59, Name = "Green Beans", Unit = EUnit.Gram },
                new Ingredient { Id = 60, Name = "Zucchini", Unit = EUnit.Gram },
                new Ingredient { Id = 61, Name = "Eggplant", Unit = EUnit.Gram },
                new Ingredient { Id = 62, Name = "Cucumber", Unit = EUnit.Piece },
                new Ingredient { Id = 63, Name = "Lettuce", Unit = EUnit.Gram },
                new Ingredient { Id = 64, Name = "Celery", Unit = EUnit.Gram },
                new Ingredient { Id = 65, Name = "Cheese", Unit = EUnit.Gram },
                new Ingredient { Id = 66, Name = "Sour Cream", Unit = EUnit.Gram },
                new Ingredient { Id = 67, Name = "Yogurt", Unit = EUnit.Gram },
                new Ingredient { Id = 68, Name = "Cottage Cheese", Unit = EUnit.Gram },
                new Ingredient { Id = 69, Name = "Cream Cheese", Unit = EUnit.Gram },
                new Ingredient { Id = 70, Name = "Parmesan Cheese", Unit = EUnit.Gram },
                new Ingredient { Id = 71, Name = "Mozzarella Cheese", Unit = EUnit.Gram },
                new Ingredient { Id = 72, Name = "Cheddar Cheese", Unit = EUnit.Gram },
                new Ingredient { Id = 73, Name = "Bacon", Unit = EUnit.Gram },
                new Ingredient { Id = 74, Name = "Ham", Unit = EUnit.Gram },
                new Ingredient { Id = 75, Name = "Sausage", Unit = EUnit.Gram },
                new Ingredient { Id = 76, Name = "Pork", Unit = EUnit.Gram },
                new Ingredient { Id = 77, Name = "Fish", Unit = EUnit.Gram },
                new Ingredient { Id = 78, Name = "Shrimp", Unit = EUnit.Gram },
                new Ingredient { Id = 79, Name = "Crab", Unit = EUnit.Gram },
                new Ingredient { Id = 80, Name = "Lobster", Unit = EUnit.Gram },
                new Ingredient { Id = 81, Name = "Tofu", Unit = EUnit.Gram },
                new Ingredient { Id = 82, Name = "Peanut Butter", Unit = EUnit.Gram },
                new Ingredient { Id = 83, Name = "Almond Butter", Unit = EUnit.Gram },
                new Ingredient { Id = 84, Name = "Cashew Butter", Unit = EUnit.Gram },
                new Ingredient { Id = 85, Name = "Sunflower Seed Butter", Unit = EUnit.Gram },
                new Ingredient { Id = 86, Name = "Vanilla Extract", Unit = EUnit.Milliliter },
                new Ingredient { Id = 87, Name = "Almond Extract", Unit = EUnit.Milliliter },
                new Ingredient { Id = 88, Name = "Nutmeg", Unit = EUnit.Gram },
                new Ingredient { Id = 89, Name = "Cinnamon", Unit = EUnit.Gram },
                new Ingredient { Id = 90, Name = "Cocoa Powder", Unit = EUnit.Gram },
                new Ingredient { Id = 91, Name = "Chocolate Chips", Unit = EUnit.Gram },
                new Ingredient { Id = 92, Name = "White Chocolate Chips", Unit = EUnit.Gram },
                new Ingredient { Id = 93, Name = "Raisins", Unit = EUnit.Gram },
                new Ingredient { Id = 94, Name = "Dried Cranberries", Unit = EUnit.Gram },
                new Ingredient { Id = 95, Name = "Dried Apricots", Unit = EUnit.Gram },
                new Ingredient { Id = 96, Name = "Dried Figs", Unit = EUnit.Gram },
                new Ingredient { Id = 97, Name = "Pecans", Unit = EUnit.Gram },
                new Ingredient { Id = 98, Name = "Walnuts", Unit = EUnit.Gram },
                new Ingredient { Id = 100, Name = "Almonds", Unit = EUnit.Gram },
                new Ingredient { Id = 101, Name = "Cashews", Unit = EUnit.Gram },
                new Ingredient { Id = 102, Name = "Pine Nuts", Unit = EUnit.Gram },
                new Ingredient { Id = 103, Name = "Peanuts", Unit = EUnit.Gram },
                new Ingredient { Id = 104, Name = "Sunflower Seeds", Unit = EUnit.Gram },
                new Ingredient { Id = 105, Name = "Pumpkin Seeds", Unit = EUnit.Gram },
                new Ingredient { Id = 106, Name = "Flaxseed", Unit = EUnit.Gram },
                new Ingredient { Id = 107, Name = "Chia Seeds", Unit = EUnit.Gram },
                new Ingredient { Id = 108, Name = "Sesame Seeds", Unit = EUnit.Gram },
                new Ingredient { Id = 109, Name = "Quinoa", Unit = EUnit.Gram },
                new Ingredient { Id = 110, Name = "Couscous", Unit = EUnit.Gram },
                new Ingredient { Id = 111, Name = "Bulgur", Unit = EUnit.Gram },
                new Ingredient { Id = 112, Name = "Farro", Unit = EUnit.Gram },
                new Ingredient { Id = 113, Name = "Barley", Unit = EUnit.Gram },
                new Ingredient { Id = 114, Name = "Oats", Unit = EUnit.Gram },
                new Ingredient { Id = 115, Name = "Cornmeal", Unit = EUnit.Gram },
                new Ingredient { Id = 116, Name = "Canned Tomatoes", Unit = EUnit.Gram },
                new Ingredient { Id = 117, Name = "Tomato Paste", Unit = EUnit.Gram },
                new Ingredient { Id = 118, Name = "Paprika", Unit = EUnit.Gram }
                );
        }
        #endregion

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
                        IngredientId = 4 
                    }
                },
                new Grocery()
                {
                    Brand = "Meat N Greet",
                    SalePrice = 40,
                    IngredientAmount = new IngredientAmount
                    {
                        Amount = 500,
                        IngredientId = 5
                    }
                },
                new Grocery()
                {
                    Brand = "Arla",
                    SalePrice = 7,
                    IngredientAmount = new IngredientAmount
                    {
                        Amount = 1000,
                        IngredientId = 6
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
                    Name = "Spaghetti Bolognese",
                    Method = """
                        1: Cook ground beef in a pan until browned.
                        2: Add chopped onion and garlic, cook until softened.
                        3: Add canned tomatoes, tomato paste, salt, pepper, and Italian seasoning. 
                        4: Simmer for 20 mins.
                        5: Cook spaghetti according to package instructions.
                        6: Serve spaghetti topped with Bolognese sauce.
                        """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() { IngredientId = 2, Amount = 125 }, // Ground Beef
                        new IngredientAmount() { IngredientId = 4, Amount = 25 }, // Onion
                        new IngredientAmount() { IngredientId = 25, Amount = 100 }, // Pasta (Spaghetti)
                        new IngredientAmount() { IngredientId = 116, Amount = 100 }, // Tomato (canned)
                        new IngredientAmount() { IngredientId = 117, Amount = 25 }, // Tomato Paste
                        new IngredientAmount() { IngredientId = 3, Amount = 1.25f }, // Salt
                        new IngredientAmount() { IngredientId = 29, Amount = 1.25f }, // Black Pepper
                        new IngredientAmount() { IngredientId = 21, Amount = 1.25f }, // Olive Oil
                        new IngredientAmount() { IngredientId = 36, Amount = 1.25f }, // Basil (part of Italian seasoning)
                        new IngredientAmount() { IngredientId = 35, Amount = 1.25f }, // Oregano (part of Italian seasoning)
                        new IngredientAmount() { IngredientId = 27, Amount = 6.25f } // Garlic
                    }
                },
                new Recipy()
                {
                    Name = "Grilled Chicken Salad",
                    Method = """
                        1: Season chicken breast with salt, pepper, and paprika. Grill until cooked through.
                        2: Chop lettuce, tomato, cucumber, and bell pepper.
                        3: In a large bowl, mix together chopped vegetables.
                        4: Slice grilled chicken and add to the salad.
                        5: Drizzle with your favorite dressing and serve.
                        """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() { IngredientId = 5, Amount = 100 }, // Chicken Breast
                        new IngredientAmount() { IngredientId = 3, Amount = 1.25f }, // Salt
                        new IngredientAmount() { IngredientId = 29, Amount = 1.25f }, // Black Pepper
                        new IngredientAmount() { IngredientId = 118, Amount = 1.25f }, // Paprika
                        new IngredientAmount() { IngredientId = 63, Amount = 50 }, // Lettuce
                        new IngredientAmount() { IngredientId = 47, Amount = 0.5f }, // Tomato
                        new IngredientAmount() { IngredientId = 62, Amount = 0.25f }, // Cucumber
                        new IngredientAmount() { IngredientId = 52, Amount = 0.25f } // Bell Pepper
                    }
                },
                new Recipy()
                {
                    Name = "Fried Rice",
                    Method = """
                        1: Cook rice according to package instructions and let it cool.
                        2: Heat oil in a pan, add chopped onion and garlic, cook until softened.
                        3: Add chopped vegetables (carrots, peas, corn, and bell pepper) and cook until tender.
                        4: Add cooked rice to the pan and stir to combine.
                        5: Add soy sauce, salt, and pepper to taste. Mix well.
                        6: Create a well in the center of the rice and pour in the beaten eggs. Scramble and mix with rice.
                        7: Serve hot.
                        """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() { IngredientId = 22, Amount = 300 },
                        new IngredientAmount() { IngredientId = 1, Amount = 100 },
                        new IngredientAmount() { IngredientId = 57, Amount = 100 },
                        new IngredientAmount() { IngredientId = 58, Amount = 100 },
                        new IngredientAmount() { IngredientId = 52, Amount = 1 },
                        new IngredientAmount() { IngredientId = 4, Amount = 50 },
                        new IngredientAmount() { IngredientId = 24, Amount = 10 },
                        new IngredientAmount() { IngredientId = 55, Amount = 50 },
                        new IngredientAmount() { IngredientId = 3, Amount = 5 },
                        new IngredientAmount() { IngredientId = 21, Amount = 5 },
                        new IngredientAmount() { IngredientId = 51, Amount = 2 }
                    }
                },
                new Recipy()
                {
                    Name = "Cheese Omelette",
                    Method = """
                        1: In a bowl, beat the eggs with salt and pepper.
                        2: Heat oil in a pan, pour in the egg mixture.
                        3: When the eggs are almost set, sprinkle grated cheese on one side.
                        4: Fold the omelette in half and cook until the cheese is melted.
                        5: Serve hot.
                        """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() { IngredientId = 51, Amount = 3 },
                        new IngredientAmount() { IngredientId = 3, Amount = 2 },
                        new IngredientAmount() { IngredientId = 21, Amount = 2 },
                        new IngredientAmount() { IngredientId = 55, Amount = 10 },
                        new IngredientAmount() { IngredientId = 72, Amount = 50 }
                    }
                },
                new Recipy()
                {
                    Name = "Caesar Salad",
                    Method = """
                        1: Chop Romaine lettuce and set aside.
                        2: In a bowl, mix together mayonnaise, lemon juice, garlic, Worcestershire sauce, Dijon mustard, and grated Parmesan cheese.
                        3: In a large bowl, toss the lettuce with the dressing.
                        4: Top with croutons and extra grated Parmesan cheese.
                        5: Serve immediately.
                        """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() { IngredientId = 61, Amount = 200 },
                        new IngredientAmount() { IngredientId = 60, Amount = 100 },
                        new IngredientAmount() { IngredientId = 41, Amount = 30 },
                        new IngredientAmount() { IngredientId = 24, Amount = 5 },
                        new IngredientAmount() { IngredientId = 38, Amount = 5 },
                        new IngredientAmount() { IngredientId = 40, Amount = 10 },
                        new IngredientAmount() { IngredientId = 70, Amount = 50 },
                        new IngredientAmount() { IngredientId = 54, Amount = 50 }
                    }
                },
                                new Recipy()
                {
                    Name = "Grilled Cheese Sandwich",
                    Method = """
                        1: Butter one side of each slice of bread.
                        2: Place a slice of cheddar cheese between the buttered sides of the bread slices.
                        3: Heat a non-stick pan over medium heat.
                        4: Place the sandwich in the pan and cook until golden brown on each side and cheese is melted.
                        5: Serve hot.
                        """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() { IngredientId = 74, Amount = 2 },
                        new IngredientAmount() { IngredientId = 8, Amount = 10 },
                        new IngredientAmount() { IngredientId = 71, Amount = 40 }
                    }
                },
                new Recipy()
                {
                    Name = "Tacos",
                    Method = """
                        1: Cook ground beef in a pan until browned. Add taco seasoning and water, simmer until thickened.
                        2: Warm taco shells in the oven for a few minutes.
                        3: Fill taco shells with seasoned beef, shredded lettuce, diced tomatoes, grated cheese, and a dollop of sour cream.
                        4: Serve immediately.
                        """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() { IngredientId = 2, Amount = 500 },
                        new IngredientAmount() { IngredientId = 33, Amount = 30 },
                        new IngredientAmount() { IngredientId = 9, Amount = 50 },
                        new IngredientAmount() { IngredientId = 78, Amount = 12 },
                        new IngredientAmount() { IngredientId = 63, Amount = 100 },
                        new IngredientAmount() { IngredientId = 47, Amount = 3 },
                        new IngredientAmount() { IngredientId = 72, Amount = 100 },
                        new IngredientAmount() { IngredientId = 75, Amount = 100 }
                    }
                },
                new Recipy()
                {
                    Name = "Pancakes",
                    Method = """
                        1: In a bowl, whisk together flour, baking powder, salt, sugar, milk, egg, and melted butter.
                        2: Heat a non-stick pan over medium heat.
                        3: Pour a small amount of batter into the pan, cook until bubbles appear on the surface, then flip and cook until golden brown.
                        4: Repeat with the remaining batter.
                        5: Serve pancakes with maple syrup or your favorite toppings.
                        """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() { IngredientId = 14, Amount = 200 },
                        new IngredientAmount() { IngredientId = 76, Amount = 10 },
                        new IngredientAmount() { IngredientId = 3, Amount = 5 },
                        new IngredientAmount() { IngredientId = 35, Amount = 20 },
                        new IngredientAmount() { IngredientId = 11, Amount = 250 },
                        new IngredientAmount() { IngredientId = 51, Amount = 1 },
                        new IngredientAmount() { IngredientId = 8, Amount = 50 },
                        new IngredientAmount() { IngredientId = 77, Amount = 50 }
                    }
                },
                new Recipy()
                {
                    Name = "Tomato Soup",
                    Method = """
                        1: In a pot, heat oil and cook chopped onion and garlic until softened.
                        2: Add canned tomatoes, vegetable broth, salt, and pepper. Simmer for 20 minutes.
                        3: Use an immersion blender to blend the soup until smooth.
                        4: Stir in heavy cream and heat through.
                        5: Serve hot with crusty bread.
                        """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() { IngredientId = 4, Amount = 50 },
                        new IngredientAmount() { IngredientId = 24, Amount = 10 },
                        new IngredientAmount() { IngredientId = 19, Amount = 800 },
                        new IngredientAmount() { IngredientId = 81, Amount = 500 },
                        new IngredientAmount() { IngredientId = 3, Amount = 5 },
                        new IngredientAmount() { IngredientId = 21, Amount = 5 },
                        new IngredientAmount() { IngredientId = 6, Amount = 100 },
                        new IngredientAmount() { IngredientId = 79, Amount = 100 }
                    }
                },
                new Recipy()
                {
                    Name = "Chicken Alfredo",
                    Method = """
                        1: Season chicken breast with salt and pepper, cook on a pan until done. Set aside.
                        2: Cook fettuccine according to package instructions.
                        3: In a pan, melt butter and add garlic, cook for 1 minute. Add heavy cream and bring to a simmer.
                        4: Stir in grated Parmesan cheese and cook until the sauce thickens.
                        5: Slice cooked chicken and add to the sauce. Mix in the cooked fettuccine.
                        6: Serve hot.
                        """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() { IngredientId = 5, Amount = 400 },
                        new IngredientAmount() { IngredientId = 3, Amount = 5 },
                        new IngredientAmount() { IngredientId = 21, Amount = 5 },
                        new IngredientAmount() { IngredientId = 80, Amount = 300 },
                        new IngredientAmount() { IngredientId = 8, Amount = 50 },
                        new IngredientAmount() { IngredientId = 24, Amount = 10 },
                        new IngredientAmount() { IngredientId = 6, Amount = 250 },
                        new IngredientAmount() { IngredientId = 70, Amount = 100 }
                    }
                },
                new Recipy() 
                { 
                    Name = "Buttered Popcorn", 
                    Method = """
                            1: Cook popcorn in microwave for 3 mins. 
                            2: Add butter to popcorn and wait until melted. 
                            3: Done.
                            """, 
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() { IngredientId = 7, Amount = 1 },
                        new IngredientAmount() { IngredientId = 8, Amount = 100 }
                    },
                },
                new Recipy() 
                { 
                    Name = "Cafe Latte", 
                    Method = """
                            1: Boil Water. 
                            2: Add instant coffee. 
                            3: Whip milk and to coffee. 
                            4: Done.
                            """,
                    IngredientAmounts = new List<IngredientAmount>()
                    {
                        new IngredientAmount() { IngredientId = 9, Amount = 50 },
                        new IngredientAmount() { IngredientId = 10, Amount = 10 },
                        new IngredientAmount() { IngredientId = 11, Amount = 70 }
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
                            IngredientId = 12,
                            Amount = 10
                        },
                        new IngredientAmount()
                        {
                            IngredientId = 13,
                            Amount = 3
                        },
                        new IngredientAmount()
                        {
                            IngredientId = 14,
                            Amount = 1000
                        },
                        new IngredientAmount()
                        {
                            IngredientId = 15,
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
