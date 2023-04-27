namespace MyFridge_Library_Data.DataModel.Abstract
{
    public abstract class DatabaseItem : SimpleDatabaseItem
    {
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
