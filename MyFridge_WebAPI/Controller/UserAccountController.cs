using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.DataModel;
using MyFridge_Library_MAUI_Client.ClientModel;
using MyFridge_WebAPI.Service.Mapper.Interface;
using MyFridge_WebAPI.Service.Data.Interface;

namespace MyFridge_WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IMapperService _map;
        private readonly ILogger<UserAccountController> _log;

        public UserAccountController(IDataService dataService, IMapperService map, ILogger<UserAccountController> log)
        {
            _dataService = dataService;
            _map = map;
            _log = log;
        }
        //create/edit
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] UserAccountDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _dataService.Users.CreateAsync(_map.ToUserAccount(from: dto));
            else
            {            
                bool success = await _dataService.Users.UpdateAsync(_map.ToUserAccount(from: dto));

                if (!success) return new JsonResult(NotFound());
            }

            await _dataService.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            UserAccount? user = await _dataService.Users.GetAsync(id);

            if (user == null) return new JsonResult(NotFound());

            return new JsonResult(_map.ToUserAccountDto(from: user));
        }
        [HttpGet]
        public async Task<JsonResult> GetByEmailAsync(string email)
        {
            UserAccount? user = await _dataService.Users.GetByEmailAsync(email);

            if (user == null) return new JsonResult(NotFound());

            return new JsonResult(_map.ToUserAccountDto(from: user));
        }
        [HttpGet]
        public async Task<JsonResult> GetOrdersAsync(int id)
        {
            UserAccount? user = await _dataService.Users.GetAsync(id);

            if (user == null) return new JsonResult(NotFound());

            return new JsonResult(_map.ToUserAccountDto(from: user).Orders);
        }
        [HttpGet]
        public async Task<JsonResult> GetFilteredAsync(string filter, int minLength = 2)
        {
            Func<UserAccount, bool> filterFunc = user =>
                user.FirstName.ToLower().Contains(filter.ToLower()) ||
                user.LastName.ToLower().Contains(filter.ToLower());

            Func<UserAccount, object> orderByFunc = user => user.LastName + " " + user.FirstName;

            List<UserAccountDto> dtos = new();
            List<UserAccount>? filteredUsers = await _dataService.Users.Query(filterFunc, orderByFunc, filter, minLength);

            if (filteredUsers == null) return new JsonResult(NotFound());

            foreach (UserAccount user in filteredUsers)
            {
                dtos.Add(_map.ToUserAccountDto(from: user));
            }
            return new JsonResult(dtos);
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<UserAccountDto> dtos = new List<UserAccountDto>();
            List<UserAccount> users = await _dataService.Users.GetAllAsync();

            foreach (UserAccount user in users)
            {
                dtos.Add(_map.ToUserAccountDto(from: user));
            }

            return new JsonResult(dtos);
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _dataService.Users.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _dataService.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}