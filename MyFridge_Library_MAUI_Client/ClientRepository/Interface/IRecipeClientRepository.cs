using MyFridge_Library_MAUI_Client.ClientRepository.Interface.Base;
using MyFridge_Library_MAUI_Client.ClientModel;

namespace MyFridge_Library_MAUI_Client.ClientRepository.Interface
{
    public interface IRecipeClientRepository : IClientRepository<RecipeDto>
    {
        public IEnumerable<RecipeDto> MakeableLazies { get; }
        public Task<IEnumerable<RecipeDto>> GetMakeableAsync(int userId);
    }
}
