using Microsoft.Extensions.Logging;
using MyFridge_Library_Data.Data.Repository.Base;
using MyFridge_Library_Data.Data.Repository.Interface;
using MyFridge_Library_Data.Model;

namespace MyFridge_Library_Data.Data.Repository
{
    public class AdminAccountRepository : Repository<AdminAccount>, IAdminAccountRepository
    {
        public AdminAccountRepository(ApplicationDbContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public override async Task<bool> UpdateAsync(AdminAccount updateEntity)
        {
            if (updateEntity == null) return false;

            AdminAccount? entityInDb = await dbSet.FindAsync(updateEntity.Id);

            if (entityInDb == null) return false;

            entityInDb.Firstname = updateEntity.Firstname;
            entityInDb.Lastname = updateEntity.Lastname;
            entityInDb.Password = updateEntity.Password;
            entityInDb.EmployeeNumber = updateEntity.EmployeeNumber;

            return true;
        }
    }
}
