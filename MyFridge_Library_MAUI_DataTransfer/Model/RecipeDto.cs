namespace MyFridge_Library_MAUI_DataTransfer.Model
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<IngredientAmountDto> IngredientAmounts { get; set; } 
            = new List<IngredientAmountDto>();
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
