using MyFridge_Library_Data.DataModel.Abstract;
using MyFridge_Library_Data.DataModel.Enum;
using System.ComponentModel.DataAnnotations;

namespace MyFridge_Library_Data.DataModel
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
