using Data_Library.Context;
using Data_Library.Repository.Abstract;
using Data_Library.Repository.Interface;
using Data_Model.Model;
using Microsoft.Extensions.Logging;

namespace Data_Library.Repository
{
    public class AdminAccountDataRepository : DataRepository<AdminAccountDto>, IAdminAccountDataRepository
    {
        public AdminAccountDataRepository(ApplicationDbContext context, ILogger log)
            : base(context, log)
        {
        }

        public override async Task<bool> UpdateAsync(AdminAccountDto updateEntity)
        {
            if (updateEntity == null) return false;

            AdminAccountDto? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.FirstName = updateEntity.FirstName;
            entityInDb.LastName = updateEntity.LastName;
            entityInDb.Password = updateEntity.Password;
            entityInDb.EmployeeNumber = updateEntity.EmployeeNumber;

            return true;
        }
    }
}
