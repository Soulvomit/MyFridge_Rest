using MyFridge_Library_Client_MAUI.ClientRepository.Abstract;
using MyFridge_Library_Client_MAUI.ClientRepository.Interface;
using MyFridge_Library_Client_MAUI.ClientModel;

namespace MyFridge_Library_Client_MAUI.ClientRepository
{
    public class GroceryClientRepository : ClientRepository<GroceryDto>, IGroceryClientRepository
    {
        public GroceryClientRepository(string baseAddress) : base(baseAddress) { }
    }
}
