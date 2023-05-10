using Data_Model.Model.Abstract;
using Data_Model.Model.Enum;

namespace Data_Model.Model
{
    public class OrderDto : DatabaseItem
    {
        public EOrderStatus Status { get; set; } = EOrderStatus.Pending;
        public virtual ICollection<GroceryDto> Groceries { get; set; } = new List<GroceryDto>();
    }
}
