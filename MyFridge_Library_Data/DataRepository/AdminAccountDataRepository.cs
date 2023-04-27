using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.DataContext;
using MyFridge_Library_Data.DataModel;
using MyFridge_Library_Data.DataRepository.Abstract;
using MyFridge_Library_Data.DataRepository.Interface;

namespace MyFridge_Library_Data.DataRepository
{
    public class AdminAccountDataRepository : DataRepository<AdminAccount>, IAdminAccountDataRepository
    {
        public AdminAccountDataRepository(ApplicationDbContext context, ILogger log)
            : base(context, log)
        {
        }

        public override async Task<bool> UpdateAsync(AdminAccount updateEntity)
        {
            if (updateEntity == null) return false;

            AdminAccount? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.FirstName = updateEntity.FirstName;
            entityInDb.LastName = updateEntity.LastName;
            entityInDb.Password = updateEntity.Password;
            entityInDb.EmployeeNumber = updateEntity.EmployeeNumber;

            return true;
        }
    }
}
