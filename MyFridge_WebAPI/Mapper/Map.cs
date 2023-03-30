using MyFridge_Library_Data.Model;
using MyFridge_Library_Data.Model.Enum;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_WebAPI.Mapper
{
    public static class Map
    {
        #region Address
        public static AddressDto? FromAddress(Address address)
        {
            if (address == null) return null;

            AddressDto dto = new AddressDto()
            {
                Id = address.Id,
                Street = address.Street,
                Extension = address.Extension,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Country = (int)address.Country
            };
            
            return dto;
        }
        public static Address? ToAddress(AddressDto dto)
        {
            if (dto == null) return null;

            Address address = new Address()
            {
                Id = dto.Id,
                Street = dto.Street,
                Extension = dto.Extension,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                Country = (ECountry)dto.Country
            };

            return address;
        }
        #endregion

        #region AdminAccount
        public static AdminAccountDto? FromAdminAccount(AdminAccount admin)
        {
            if (admin == null) return null;

            AdminAccountDto dto = new AdminAccountDto()
            {
                Id = admin.Id,
                Firstname = admin.Firstname,
                Lastname = admin.Lastname,
                Password = admin.Password,
                EmployeeNumber = admin.EmployeeNumber
            };

            return dto;
        }
        public static AdminAccount? ToAdminAccount(AdminAccountDto dto)
        {
            if (dto == null) return null;

            AdminAccount admin = new AdminAccount()
            {
                Id = dto.Id,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Password = dto.Password,
                EmployeeNumber = dto.EmployeeNumber
            };

            return admin;
        }
        #endregion

        #region UserAccount
        public static UserAccountDto? FromUserAccount(UserAccount user)
        {
            if (user == null) return null;

            UserAccountDto dto = new UserAccountDto()
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                Address = FromAddress(user.Address!)
            };
            foreach(Order order in user.Orders)
            {
                dto.Orders.Add(FromOrder(order));
            }
            foreach(IngredientAmount ingredientAmount in user.IngredientAmounts)
            {
                IngredientDto idto = FromIngredient(ingredientAmount.Ingredient!)!;
                idto.Amount = ingredientAmount.Amount;
                dto.Ingredients.Add(idto);
            }
            return dto;
        }
        public static UserAccount? ToUserAccount(UserAccountDto dto)
        {
            if (dto == null) return null;

            UserAccount user = new UserAccount()
            {
                Id = dto.Id,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Password = dto.Password,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                BirthDate = dto.BirthDate
            };
            foreach (OrderDto odto in dto.Orders)
            {
                user.Orders.Add(ToOrder(odto)!);
            }
            foreach (IngredientDto idto in dto.Ingredients)
            {
                IngredientAmount ingredientAmount = new IngredientAmount 
                {
                    Ingredient = ToIngredient(idto),
                    Amount = idto.Amount
                };
                user.IngredientAmounts.Add(ingredientAmount);
            }
            return user;
        }
        #endregion

        #region Grocery
        public static GroceryDto? FromGrocery(Grocery grocery)
        {
            if (grocery == null) return null;

            GroceryDto dto = new GroceryDto();
            dto.Id = grocery.Id;
            dto.Brand = grocery.Brand;
            dto.SalePriceDKK = grocery.SalePriceDKK;
            dto.Ingredient = FromIngredient(grocery.IngredientAmount!.Ingredient!);
            dto.Ingredient!.Amount = grocery.IngredientAmount.Amount;

            return dto;
        }
        public static Grocery? ToGrocery(GroceryDto dto)
        {
            if (dto == null) return null;

            Grocery grocery = new Grocery
            {
                Id = dto.Id,
                Brand = dto.Brand,
                SalePriceDKK = dto.SalePriceDKK,
                IngredientAmount = ToIngredientAmount(dto.Ingredient)
            };

            return grocery;
        }
        #endregion

        #region Ingredient
        public static IngredientDto? FromIngredient(Ingredient ingredient)
        {
            if (ingredient == null) return null;

            IngredientDto dto = new IngredientDto()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Unit = (int)ingredient.Unit,
                Amount = 0,
                ExpirationDate = null
            };

            return dto;
        }
        public static Ingredient? ToIngredient(IngredientDto dto)
        {
            if (dto == null) return null;

            Ingredient ingredient = new Ingredient()
            {
                Id = dto.Id,
                Name = dto.Name,
                Unit = (EUnit)dto.Unit
            };

            return ingredient;
        }
        public static IngredientDto? FromIngredientAmount(IngredientAmount ingredientAmount)
        {
            if (ingredientAmount == null) return null;

            IngredientDto dto = new IngredientDto()
            {
                Id = ingredientAmount.Id,
                Name = ingredientAmount.Ingredient!.Name,
                Unit = (int)ingredientAmount.Ingredient!.Unit,
                Amount = ingredientAmount.Amount,
                ExpirationDate = ingredientAmount.ExpirationDate
            };

            return dto;
        }
        public static IngredientAmount? ToIngredientAmount(IngredientDto dto)
        {
            if (dto == null) return null;

            IngredientAmount ingredient = new IngredientAmount
            {
                Id = dto.Id,
                Ingredient = ToIngredient(dto),
                Amount = dto.Amount,
                ExpirationDate = dto.ExpirationDate
            };

            return ingredient;
        }
        #endregion

        #region Order
        public static OrderDto? FromOrder(Order order)
        {
            if (order == null) return null;

            OrderDto dto = new OrderDto()
            {
                Id = order.Id,
                Created = order.CreationTime,
                Status = (int)order.Status
            };
            foreach(Grocery grocery in order.Groceries)
            {
                dto.Groceries.Add(FromGrocery(grocery));
            }

            return dto;
        }
        public static Order? ToOrder(OrderDto dto)
        {
            if (dto == null) return null;

            Order order = new Order()
            {
                Id = dto.Id,
                Status = (EOrderStatus)dto.Status
            };
            foreach (GroceryDto gdto in dto.Groceries)
            {
                if (gdto != null)
                {
                    order.Groceries.Add(ToGrocery(gdto)!);
                }
            }

            return order;
        }
        #endregion

        #region Recipy
        public static RecipyDto? FromRecipy(Recipy recipy)
        {
            if (recipy == null) return null;

            RecipyDto dto = new RecipyDto()
            {
                Id = recipy.Id,
                Name = recipy.Name,
                Description = recipy.Description
            };
            foreach (IngredientAmount ingredientAmount in recipy.IngredientAmounts)
            {
                IngredientDto idto = FromIngredient(ingredientAmount.Ingredient!)!;
                idto.Amount = ingredientAmount.Amount;
                dto.Ingredients.Add(idto);
            }

            return dto;
        }
        public static Recipy? ToRecipy(RecipyDto dto)
        {
            if (dto == null) return null;

            Recipy recipy = new Recipy()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description
            };

            foreach (IngredientDto idto in dto.Ingredients)
            {
                IngredientAmount ingredientAmount = new IngredientAmount
                {
                    Ingredient = ToIngredient(idto),
                    Amount = idto.Amount
                };
                recipy.IngredientAmounts.Add(ingredientAmount);
            }
            return recipy;
        }
        #endregion
    }
}
