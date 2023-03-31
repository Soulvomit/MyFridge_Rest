namespace MyFridge_Library_MAUI_DataTransfer.DataTransferObject
{
    public class RecipyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
