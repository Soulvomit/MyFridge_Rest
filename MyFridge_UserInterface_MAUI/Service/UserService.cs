using MyFridge_Library_MAUI_Client;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.ViewModel;

namespace MyFridge_UserInterface_MAUI.Service
{
    public sealed class UserService
    {
        //private static readonly object mutex = new object();
        //private static UserService instance = null;
        //public static UserService Instance
        //{
        //    get
        //    {
        //        lock (mutex)
        //        {
        //            if (instance == null)
        //            {
        //                instance = new UserService();
        //            }
        //            return instance;
        //        }
        //    }
        //}
        public UserAccountDto User { get; set; }
        public UserAccountApiClient UserClient { get; private set; }
        public IngredientApiClient IngredientClient { get; private set; }
        public RecipyApiClient RecipyClient { get; private set; }

        public UserService()
        {
            string baseAddr = "https://localhost:44364/";
            UserClient = new UserAccountApiClient(baseAddr);
            IngredientClient = new IngredientApiClient(baseAddr);
            RecipyClient = new RecipyApiClient(baseAddr);
        }
    }
}
