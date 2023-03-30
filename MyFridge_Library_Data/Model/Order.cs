using MyFridge_Library_Data.Model.Abstract;
using MyFridge_Library_Data.Model.Enum;

namespace MyFridge_Library_Data.Model
{
    public class Order : DatabaseItem
    {
        public EOrderStatus Status { get; set; } = EOrderStatus.Pending;
        public virtual ICollection<Grocery> Groceries { get; set; } = new List<Grocery>();
    }
}
