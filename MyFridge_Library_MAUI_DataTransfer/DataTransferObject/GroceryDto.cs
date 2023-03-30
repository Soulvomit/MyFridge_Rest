namespace MyFridge_Library_MAUI_DataTransfer.DataTransferObject
{
    public class GroceryDto
    {
        public int Id { get; set; }
        public IngredientDto Ingredient { get; set; }
        public string Brand { get; set; }
        public float SalePriceDKK { get; set; }
    }
}
