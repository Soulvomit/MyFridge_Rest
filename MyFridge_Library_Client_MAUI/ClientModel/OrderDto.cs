namespace MyFridge_Library_Client_MAUI.ClientModel
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int Status { get; set; }
        public List<GroceryDto> Groceries { get; set; } = new List<GroceryDto>();
        public float TotalPriceDkk
        {
            get
            {
                float total = 0;

                foreach (var item in Groceries)
                    total += item.SalePrice;

                return total;
            }
        }
    }
}
