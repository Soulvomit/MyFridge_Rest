using Client_Model.Model;
using Data_Interface.Service.Data.Interface;
using Data_Interface.Service.Mapper.Interface;
using Data_Model.Model;
using Microsoft.AspNetCore.Mvc;

namespace Data_Interface.Controller
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
        public async Task<JsonResult> UpsertAsync([FromBody] AdminAccountCto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _dataService.Admins.CreateAsync(_map.ToAdminAccountDto(from: dto));
            else
            {
                bool success = await _dataService.Admins.UpdateAsync(_map.ToAdminAccountDto(from: dto));

                if (!success) return new JsonResult(NotFound());
            }

            await _dataService.CompleteAsync();

            return new JsonResult((dto));
        }
        //get
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            AdminAccountDto? admin = await _dataService.Admins.GetAsync(id);

            if (admin == null) return new JsonResult(NotFound());

            return new JsonResult(_map.ToAdminAccountCto(from: admin));
        }
        //get all
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<AdminAccountCto> dtos = new List<AdminAccountCto>();
            List<AdminAccountDto> admins = await _dataService.Admins.GetAllAsync();

            foreach (AdminAccountDto admin in admins)
            {
                dtos.Add(_map.ToAdminAccountCto(from: admin));
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
