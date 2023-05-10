using Client_Library.Repository.Interface.Base;
using Client_Model.Model;

namespace Client_Library.Repository.Interface
{
    public interface IRecipeClientRepository : IClientRepository<RecipeCto>
    {
        public IEnumerable<RecipeCto> MakeableLazies { get; }
        public Task<IEnumerable<RecipeCto>> GetMakeableAsync(int userId);
    }
}
