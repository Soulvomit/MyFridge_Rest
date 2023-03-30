using MyFridge_Library_Data.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFridge_Library_Data.Model
{
    public class Grocery : DatabaseItem
    {
        [Required]
        public int IngredientAmountId { get; set; }
        [Required, ForeignKey("IngredientAmountId")]
        public required virtual IngredientAmount? IngredientAmount { get; set; }
        [MaxLength(50)]
        public string? Brand { get; set; } = string.Empty;
        public float SalePriceDKK { get; set; } = 0;
    }
}