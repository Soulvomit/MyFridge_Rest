using Data_Model.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Model.Model
{
    public class IngredientAmountDto : SimpleDatabaseItem
    {
        [Required]
        public int IngredientId { get; set; }
        [Required, ForeignKey("IngredientId")]
        public virtual IngredientDto? Ingredient { get; set; }
        public required float Amount { get; set; } = 0;
        public DateTime? ExpirationDate { get; set; } = null;
    }
}