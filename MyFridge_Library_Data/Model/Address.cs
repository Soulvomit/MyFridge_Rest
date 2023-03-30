using MyFridge_Library_Data.Model.Abstract;
using MyFridge_Library_Data.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace MyFridge_Library_Data.Model
{
    public class Address : DatabaseItem
    {
        [MaxLength(50)]
        public string? Street { get; set; }
        [MaxLength(50)]
        public string? Extension { get; set; }
        [MaxLength(50)]
        public string? City { get; set; }
        [MaxLength(50)]
        public string? State { get; set; }
        [MaxLength(50)]
        public string? ZipCode { get; set; }
        public ECountry Country { get; set; } = ECountry.UnitedStates;
    }
}