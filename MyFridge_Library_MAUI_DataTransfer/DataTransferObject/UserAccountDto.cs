namespace MyFridge_Library_MAUI_DataTransfer.DataTransferObject
{
    public class UserAccountDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ulong PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public AddressDto Address { get; set; }
        public List<IngredientAmountDto> Ingredients { get; set; } 
            = new List<IngredientAmountDto>();
        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }
}
