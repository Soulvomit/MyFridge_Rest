using MyFridge_Library_Data.DataModel;
using MyFridge_Library_Client_MAUI.ClientModel;

namespace MyFridge_Interface_WebAPI.Service.Mapper.Interface
{
    public interface IMapperService
    {
        public AddressDto? ToAddressDto(Address? from);
        public Address? ToAddress(AddressDto? from);

        public AdminAccountDto? ToAdminAccountDto(AdminAccount? from);
        public AdminAccount? ToAdminAccount(AdminAccountDto? from);

        public UserAccountDto? ToUserAccountDto(UserAccount? from);
        public UserAccount? ToUserAccount(UserAccountDto? from);

        public GroceryDto? ToGroceryDto(Grocery? from);
        public Grocery? ToGrocery(GroceryDto? from);

        public IngredientDto? ToIngredientDto(Ingredient? from);
        public Ingredient? ToIngredient(IngredientDto? from);

        public IngredientAmountDto? ToIngredientAmountDto(IngredientAmount? from);
        public IngredientAmount? ToIngredientAmount(IngredientAmountDto? from);

        public OrderDto? ToOrderDto(Order? from);
        public Order? ToOrder(OrderDto? from);

        public RecipeDto? ToRecipeDto(Recipe? from);
        public Recipe? ToRecipe(RecipeDto? from);
    }
}
