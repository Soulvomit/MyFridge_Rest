using MyFridge_Library_MAUI_Client.Base;
using MyFridge_Library_MAUI_Client.Interface;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_Library_MAUI_Client
{
    public class GroceryApiClient : ApiClient<GroceryDto>, IGroceryApiClient
    {
        public GroceryApiClient(string baseAddress): base(baseAddress) { }
    }
}
