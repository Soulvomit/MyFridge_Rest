using MyFridge_Library_Data.Model.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MyFridge_Library_Data.Model
{
    public class Recipy : DatabaseItem
    {
        [Required, MaxLength(50)]
        public required string Name { get; set; }
        public virtual ICollection<IngredientAmount> IngredientAmounts { get; set; } = new List<IngredientAmount>();
        [MaxLength(1800)]
        public string? Description { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? ImageUrl { get; set; } = string.Empty:

    }
}