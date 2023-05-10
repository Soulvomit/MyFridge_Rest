﻿using Client_Model.Model.Interface;

namespace Client_Model.Model
{
    public class RecipeCto : ICto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<IngredientAmountCto> IngredientAmounts { get; set; } 
            = new List<IngredientAmountCto>();
        public string Method { get; set; }
        public string ImageUrl { get; set; }
    }
}
