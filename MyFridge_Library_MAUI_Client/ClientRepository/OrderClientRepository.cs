using MyFridge_Library_MAUI_Client.ClientRepository.Abstract;
using MyFridge_Library_MAUI_Client.ClientRepository.Interface;
using MyFridge_Library_MAUI_Client.ClientModel;

namespace MyFridge_Library_MAUI_Client.ClientRepository
{
    public class OrderClientRepository : ClientRepository<OrderDto>, IOrderClientRepository
    {
        public OrderClientRepository(string baseAddress) : base(baseAddress) { }
    }
}
