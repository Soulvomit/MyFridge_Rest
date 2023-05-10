using Client_Model.Model.Interface;

namespace Client_Model.Model
{
    public class UserAccountCto : ICto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ulong PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public AddressCto Address { get; set; }
        public List<IngredientAmountCto> IngredientAmounts { get; set; } 
            = new List<IngredientAmountCto>();
        public List<OrderCto> Orders { get; set; } = new List<OrderCto>();
    }
}
