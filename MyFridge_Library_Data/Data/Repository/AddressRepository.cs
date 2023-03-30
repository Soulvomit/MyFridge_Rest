using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.Data.Repository.Base;
using MyFridge_Library_Data.Data.Repository.Interface;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public override async Task<bool> UpdateAsync(Address updateEntity)
        {
            if (updateEntity == null) return false;

            Address? entityInDb = await dbSet.FindAsync(updateEntity.Id);

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
