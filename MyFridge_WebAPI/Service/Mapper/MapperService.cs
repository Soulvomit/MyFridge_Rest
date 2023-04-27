using MyFridge_Library_Data.DataModel;
using MyFridge_Library_Data.DataModel.Enum;
using MyFridge_Library_MAUI_Client.ClientModel;
using MyFridge_WebAPI.Service.Mapper.Interface;

namespace MyFridge_WebAPI.Service.Mapper
{
    public class MapperService: IMapperService
    {
        #region Address
        public AddressDto? ToAddressDto(Address? from)
        {
            if (from == null) return null;

            AddressDto dto = new()
            {
                Id = from.Id,
                Street = from.Street,
                Extension = from.Extension,
                City = from.City,
                State = from.State,
                ZipCode = from.ZipCode,
                Country = (int)from.Country
            };

            return dto;
        }
        public Address? ToAddress(AddressDto? from)
        {
            if (from == null) return null;

            Address address = new()
            {
                Id = from.Id,
                Street = from.Street,
                Extension = from.Extension,
                City = from.City,
                State = from.State,
                ZipCode = from.ZipCode,
                Country = (ECountry)from.Country
            };

            return address;
        }
        #endregion

        #region AdminAccount
        public AdminAccountDto? ToAdminAccountDto(AdminAccount? from)
        {
            if (from == null) return null;

            AdminAccountDto dto = new()
            {
                Id = from.Id,
                FirstName = from.FirstName,
                LastName = from.LastName,
                Password = from.Password,
                EmployeeNumber = from.EmployeeNumber
            };

            return dto;
        }
        public AdminAccount? ToAdminAccount(AdminAccountDto? from)
        {
            if (from == null) return null;

            AdminAccount admin = new()
            {
                Id = from.Id,
                FirstName = from.FirstName,
                LastName = from.LastName,
                Password = from.Password,
                EmployeeNumber = from.EmployeeNumber
            };

            return admin;
        }
        #endregion

        #region UserAccount
        public UserAccountDto? ToUserAccountDto(UserAccount? from)
        {
            if (from == null) return null;

            UserAccountDto dto = new()
            {
                Id = from.Id,
                FirstName = from.FirstName,
                LastName = from.LastName,
                Password = from.Password,
                Email = from.Email,
                PhoneNumber = from.PhoneNumber,
                BirthDate = from.BirthDate,
                Address = ToAddressDto(from.Address!)
            };
            foreach (Order order in from.Orders)
            {
                dto.Orders.Add(ToOrderDto(from: order));
            }
            foreach (IngredientAmount ingredientAmount in from.IngredientAmounts)
            {
                dto.IngredientAmounts.Add(ToIngredientAmountDto(from: ingredientAmount));
            }
            return dto;
        }
        public UserAccount? ToUserAccount(UserAccountDto? from)
        {
            if (from == null) return null;

            UserAccount user = new()
            {
                Id = from.Id,
                FirstName = from.FirstName,
                LastName = from.LastName,
                Password = from.Password,
                Email = from.Email,
                PhoneNumber = from.PhoneNumber,
                BirthDate = from.BirthDate
            };
            foreach (OrderDto dto in from.Orders)
            {
                user.Orders.Add(ToOrder(from: dto));
            }
            foreach (IngredientAmountDto dto in from.IngredientAmounts)
            {
                user.IngredientAmounts.Add(ToIngredientAmount(from: dto));
            }
            return user;
        }
        #endregion

        #region Grocery
        public GroceryDto? ToGroceryDto(Grocery? from)
        {
            if (from == null) return null;

            GroceryDto dto = new()
            {
                Id = from.Id,
                Brand = from.Brand,
                SalePrice = from.SalePrice,
                Category = from.Category,
                ItemIdentifier = from.ItemIdentifier,
                ImageUrl = from.ImageUrl,
                Ingredient = ToIngredientAmountDto(from.IngredientAmount)
            };
            return dto;
        }
        public Grocery? ToGrocery(GroceryDto? from)
        {
            if (from == null) return null;

            Grocery grocery = new()
            {
                Id = from.Id,
                Brand = from.Brand,
                SalePrice = from.SalePrice,
                Category = from.Category,
                ItemIdentifier = from.ItemIdentifier,
                ImageUrl = from.ImageUrl,
                IngredientAmount = ToIngredientAmount(from.Ingredient)
            };

            return grocery;
        }
        #endregion

        #region Ingredient
        public IngredientDto? ToIngredientDto(Ingredient? from)
        {
            if (from == null) return null;

            IngredientDto dto = new()
            {
                Id = from.Id,
                Name = from.Name,
                Unit = (int)from.Unit
            };

            return dto;
        }
        public Ingredient? ToIngredient(IngredientDto? from)
        {
            if (from == null) return null;

            Ingredient ingredient = new()
            {
                Id = from.Id,
                Name = from.Name,
                Unit = (EUnit)from.Unit
            };

            return ingredient;
        }
        #endregion

        #region IngredientAmount
        public IngredientAmountDto? ToIngredientAmountDto(IngredientAmount? from)
        {
            if (from == null) return null;

            IngredientAmountDto dto = new()
            {
                Id = from.Id,
                Ingredient = ToIngredientDto(from.Ingredient),
                Amount = from.Amount,
                ExpirationDate = from.ExpirationDate
            };

            return dto;
        }
        public IngredientAmount? ToIngredientAmount(IngredientAmountDto? from)
        {
            if (from == null) return null;

            IngredientAmount ingredient = new()
            {
                Id = from.Id,
                Ingredient = ToIngredient(from.Ingredient),
                Amount = from.Amount,
                ExpirationDate = from.ExpirationDate
            };

            return ingredient;
        }
        #endregion

        #region Order
        public OrderDto? ToOrderDto(Order? from)
        {
            if (from == null) return null;

            OrderDto dto = new()
            {
                Id = from.Id,
                Created = from.CreationTime,
                Status = (int)from.Status
            };
            foreach (Grocery grocery in from.Groceries)
            {
                dto.Groceries.Add(ToGroceryDto(from: grocery));
            }

            return dto;
        }
        public Order? ToOrder(OrderDto? from)
        {
            if (from == null) return null;

            Order order = new()
            {
                Id = from.Id,
                Status = (EOrderStatus)from.Status
            };
            foreach (GroceryDto dto in from.Groceries)
            {
                order.Groceries.Add(ToGrocery(from: dto));
            }

            return order;
        }
        #endregion

        #region Recipy
        public RecipeDto? ToRecipeDto(Recipe? from)
        {
            if (from == null) return null;

            RecipeDto dto = new()
            {
                Id = from.Id,
                Name = from.Name,
                Method = from.Method,
                ImageUrl = from.ImageUrl
            };
            foreach (IngredientAmount ingredientAmount in from.IngredientAmounts)
            {
                dto.IngredientAmounts.Add(ToIngredientAmountDto(from: ingredientAmount));
            }

            return dto;
        }
        public Recipe? ToRecipe(RecipeDto? from)
        {
            if (from == null) return null;

            Recipe recipe = new()
            {
                Id = from.Id,
                Name = from.Name,
                Method = from.Method,
                ImageUrl = from.ImageUrl
            };

            foreach (IngredientAmountDto dto in from.IngredientAmounts)
            {
                recipe.IngredientAmounts.Add(ToIngredientAmount(from: dto));
            }
            return recipe;
        }
        #endregion
    }
}
