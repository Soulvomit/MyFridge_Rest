using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFridge_Library_Data.Model
{
    public class UserAccount : Abstract.Account
    {
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address? Address { get; set; }
        [Required, MaxLength(100)]
        public required string? Email { get; set; }
        [Required]
        public required ulong PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<IngredientAmount> IngredientAmounts { get; set; } = new List<IngredientAmount>();
    }
}