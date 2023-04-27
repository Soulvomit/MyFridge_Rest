using MyFridge_Library_MAUI_Client.ClientRepository.Abstract;
using MyFridge_Library_MAUI_Client.ClientRepository.Interface;
using MyFridge_Library_MAUI_Client.ClientModel;

namespace MyFridge_Library_MAUI_Client.ClientRepository
{
    public class AdminAccountClientRepository : ClientRepository<AdminAccountDto>, IAdminAccountClientRepository
    {
        public AdminAccountClientRepository(string baseAddress) : base(baseAddress) { }
    }
}
