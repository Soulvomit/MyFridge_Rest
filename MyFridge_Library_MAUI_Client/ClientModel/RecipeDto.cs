namespace MyFridge_Library_MAUI_Client.ClientModel
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<IngredientAmountDto> IngredientAmounts { get; set; } 
            = new List<IngredientAmountDto>();
        public string Method { get; set; }
        public string ImageUrl { get; set; }
    }
}
