﻿using MyFridge_Library_Data.DataModel.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MyFridge_Library_Data.DataModel
{
    public class Recipe : DatabaseItem
    {
        [Required, MaxLength(50)]
        public required string Name { get; set; }
        public virtual ICollection<IngredientAmount> IngredientAmounts { get; set; } = new List<IngredientAmount>();
        [MaxLength(1800)]
        public string? Method { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? ImageUrl { get; set; } = string.Empty;

    }
}