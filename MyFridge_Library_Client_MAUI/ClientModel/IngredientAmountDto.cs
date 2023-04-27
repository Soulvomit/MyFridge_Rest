namespace MyFridge_Library_Client_MAUI.ClientModel
{
    public class IngredientAmountDto
    {
        public int Id { get; set; }
        public IngredientDto Ingredient { get; set; }
        public float Amount { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
