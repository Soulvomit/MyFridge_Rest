namespace MyFridge_Library_MAUI_DataTransfer.Model
{
    public class UserAccountDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ulong PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public AddressDto Address { get; set; }
        public List<IngredientAmountDto> IngredientAmounts { get; set; } 
            = new List<IngredientAmountDto>();
        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }
}
