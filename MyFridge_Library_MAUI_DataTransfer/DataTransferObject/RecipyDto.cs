namespace MyFridge_Library_MAUI_DataTransfer.DataTransferObject
{
    public class RecipyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<IngredientAmountDto> Ingredients { get; set; } 
            = new List<IngredientAmountDto>();
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
