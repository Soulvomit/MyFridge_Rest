using MyFridge_Library_MAUI_Client.ClientRepository.Abstract;
using MyFridge_Library_MAUI_Client.ClientRepository.Interface;
using MyFridge_Library_MAUI_Client.ClientModel;

namespace MyFridge_Library_MAUI_Client.ClientRepository
{
    public class GroceryClientRepository : ClientRepository<GroceryDto>, IGroceryClientRepository
    {
        public GroceryClientRepository(string baseAddress) : base(baseAddress) { }
    }
}
