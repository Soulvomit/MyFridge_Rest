using System.ComponentModel.DataAnnotations;

namespace MyFridge_Library_Data.Model
{
    public class AdminAccount : Abstract.Account
    {
        [Required, MaxLength(100)]
        public required string EmployeeNumber { get; set; }
    }
}
