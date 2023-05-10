using Client_Model.Model.Interface;

namespace Client_Model.Model
{
    public class OrderCto : ICto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int Status { get; set; }
        public List<GroceryCto> Groceries { get; set; } = new List<GroceryCto>();
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
