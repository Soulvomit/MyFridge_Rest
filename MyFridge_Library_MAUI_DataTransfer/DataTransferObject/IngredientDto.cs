namespace MyFridge_Library_MAUI_DataTransfer.DataTransferObject
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
        public float Amount { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string ExpirationDateStr
        {
            get 
            {
                if (ExpirationDate == null)
                    return null;

                return ((DateTime)ExpirationDate).ToShortDateString(); 
            }
        }
        public string UnitStr 
        { 
            get 
            {
                if (Unit == 0)
                    if (Amount < 2)
                        return "piece";
                    else
                        return "pieces";
                if (Unit == 1)
                    return "ml";
                else
                    if (Amount < 2)
                        return "gram";
                    else
                        return "grams";
            } 
        }
    }
}
