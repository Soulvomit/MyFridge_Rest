using Client_Model.Model.Interface;

namespace Client_Model.Model
{
    public class IngredientCto : ICto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
        public string Category { get; set; }
    }
}
