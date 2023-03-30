using System.ComponentModel.DataAnnotations;

namespace MyFridge_Library_Data.Model.Abstract
{
    public abstract class Account : DatabaseItem
    {
        [MaxLength(50), Required]
        public required string Firstname { get; set; }
        [MaxLength(50), Required]
        public required string Lastname { get; set; }
        [MaxLength(30), Required]
        public required string Password { get; set; }
    }
}