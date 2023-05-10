using Client_Model.Model.Interface;

namespace Client_Model.Model
{
    public class IngredientAmountCto : ICto
    {
        public int Id { get; set; }
        public IngredientCto Ingredient { get; set; }
        public float Amount { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
