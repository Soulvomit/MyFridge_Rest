using MyFridge_Library_MAUI_Client.Base;
using MyFridge_Library_MAUI_Client.Interface;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;

namespace MyFridge_Library_MAUI_Client
{
    public class OrderApiClient : ApiClient<OrderDto>, IOrderApiClient
    {
        public OrderApiClient(string baseAddress) : base(baseAddress) { }
    }
}
