using System.ComponentModel.DataAnnotations;

namespace MyFridge_Library_Data.DataModel
{
    public class AdminAccount : Abstract.Account
    {
        [Required, MaxLength(100)]
        public required string EmployeeNumber { get; set; }
    }
}
