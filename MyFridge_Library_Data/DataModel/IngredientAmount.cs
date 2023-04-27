using MyFridge_Library_Data.DataModel.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFridge_Library_Data.DataModel
{
    public class IngredientAmount : SimpleDatabaseItem
    {
        [Required]
        public int IngredientId { get; set; }
        [Required, ForeignKey("IngredientId")]
        public virtual Ingredient? Ingredient { get; set; }
        public required float Amount { get; set; } = 0;
        public DateTime? ExpirationDate { get; set; } = null;
    }
}