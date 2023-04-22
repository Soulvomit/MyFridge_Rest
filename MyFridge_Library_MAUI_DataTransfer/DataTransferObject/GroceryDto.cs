namespace MyFridge_Library_MAUI_DataTransfer.DataTransferObject
{
    public class GroceryDto
    {
        public int Id { get; set; }
        public IngredientAmountDto Ingredient { get; set; }
        public string Brand { get; set; }
        public string ItemIdentifier { get; set; }
        public float SalePrice { get; set; }
        public string ImageUrl { get; set; }
    }
}
