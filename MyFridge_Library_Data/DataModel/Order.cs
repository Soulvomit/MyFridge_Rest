using MyFridge_Library_Data.DataModel.Abstract;
using MyFridge_Library_Data.DataModel.Enum;

namespace MyFridge_Library_Data.DataModel
{
    public class Order : DatabaseItem
    {
        public EOrderStatus Status { get; set; } = EOrderStatus.Pending;
        public virtual ICollection<Grocery> Groceries { get; set; } = new List<Grocery>();
    }
}
