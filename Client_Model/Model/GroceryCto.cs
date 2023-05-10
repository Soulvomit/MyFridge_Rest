using Client_Model.Model.Interface;

namespace Client_Model.Model
{
    public class GroceryCto : ICto
    {
        public int Id { get; set; }
        public IngredientAmountCto IngredientAmount { get; set; }
        public string Brand { get; set; }
        public string ItemIdentifier { get; set; }
        public float SalePrice { get; set; }
        public string ImageUrl { get; set; }
    }
}
