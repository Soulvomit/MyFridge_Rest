using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.DataModel;
using MyFridge_Library_MAUI_Client.ClientModel;
using MyFridge_WebAPI.Service.Mapper.Interface;
using MyFridge_WebAPI.Service.Data.Interface;

namespace MyFridge_WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminAccountController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IMapperService _map;
        private readonly ILogger<AdminAccountController> _log;

        public AdminAccountController(IDataService dataService, IMapperService map, ILogger<AdminAccountController> log)
        {
            _dataService = dataService;
            _map = map;
            _log = log;
        }

        //create/edit
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] AdminAccountDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _dataService.Admins.CreateAsync(_map.ToAdminAccount(from: dto));
            else
            {
                bool success = await _dataService.Admins.UpdateAsync(_map.ToAdminAccount(from: dto));

                if (!success) return new JsonResult(NotFound());
            }

            await _dataService.CompleteAsync();

            return new JsonResult((dto));
        }
        //get
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            AdminAccount? admin = await _dataService.Admins.GetAsync(id);

            if (admin == null) return new JsonResult(NotFound());

            return new JsonResult(_map.ToAdminAccountDto(from: admin));
        }
        //get all
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<AdminAccountDto> dtos = new List<AdminAccountDto>();
            List<AdminAccount> admins = await _dataService.Admins.GetAllAsync();

            foreach (AdminAccount admin in admins)
            {
                dtos.Add(_map.ToAdminAccountDto(from: admin));
            }

            return new JsonResult(dtos);
        }
        //delete
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _dataService.Admins.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _dataService.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}
