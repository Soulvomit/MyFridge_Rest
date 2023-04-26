using MyFridge_Library_Data.Model.Abstract;
using MyFridge_Library_Data.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace MyFridge_Library_Data.Model
{
    public class Ingredient : SimpleDatabaseItem
    {
        //add category to this
        [Required, MaxLength(50)]
        public required string Name { get; set; }
        [Required]
        public required EUnit Unit { get; set; } = EUnit.Piece;
    }
}
