using Data_Library.Context;
using Data_Library.Repository.Abstract;
using Data_Library.Repository.Interface;
using Data_Model.Model;
using Microsoft.Extensions.Logging;

namespace Data_Library.Repository
{
    public class AddressDataRepository : DataRepository<AddressDto>, IAddressDataRepository
    {
        public AddressDataRepository(ApplicationDbContext context, ILogger log)
            : base(context, log)
        {
        }

        public override async Task<bool> UpdateAsync(AddressDto updateEntity)
        {
            if (updateEntity == null) return false;

            AddressDto? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Street = updateEntity.Street;
            entityInDb.Extension = updateEntity.Extension;
            entityInDb.City = updateEntity.City;
            entityInDb.State = updateEntity.State;
            entityInDb.ZipCode = updateEntity.ZipCode;
            entityInDb.Country = updateEntity.Country;

            return true;
        }
    }
}
