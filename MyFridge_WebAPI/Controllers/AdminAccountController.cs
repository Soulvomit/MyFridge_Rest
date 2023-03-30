using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.Model;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_WebAPI.Mapper;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminAccountController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<AdminAccountController> _logger;

        public AdminAccountController(IUnitOfWork uow, ILogger<AdminAccountController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        //create/edit
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] AdminAccountDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _uow.Admins.CreateAsync(Map.ToAdminAccount(dto)!);
            else
            {
                bool success = await _uow.Admins.UpdateAsync(Map.ToAdminAccount(dto)!);

                if (!success) return new JsonResult(NotFound());
            }

            await _uow.CompleteAsync();

            return new JsonResult((dto));
        }
        //get
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            AdminAccount? admin = await _uow.Admins.GetAsync(id);

            if (admin == null) return new JsonResult(NotFound());

            return new JsonResult((Map.FromAdminAccount(admin)));
        }
        //get all
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<AdminAccountDto> dtos = new List<AdminAccountDto>();
            List<AdminAccount> admins = await _uow.Admins.GetAllAsync();

            foreach (AdminAccount admin in admins)
            {
                dtos.Add(Map.FromAdminAccount(admin)!);
            }

            return new JsonResult((dtos));
        }
        //delete
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _uow.Admins.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}
