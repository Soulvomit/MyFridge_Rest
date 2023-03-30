using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.Model;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_WebAPI.Mapper;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UserAccountController> _logger;

        public UserAccountController(IUnitOfWork uow, ILogger<UserAccountController> logger)
        {
            _uow = uow;
            _logger = logger;
        }
        //create/edit
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] UserAccountDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _uow.Users.CreateAsync(Map.ToUserAccount(dto)!);
            else
            {            
                bool success = await _uow.Users.UpdateAsync(Map.ToUserAccount(dto)!);

                if (!success) return new JsonResult(NotFound());
            }

            await _uow.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            UserAccount? user = await _uow.Users.GetAsync(id);

            if (user == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromUserAccount(user));
        }
        [HttpGet]
        public async Task<JsonResult> GetByEmailAsync(string email)
        {
            UserAccount? user = await _uow.Users.GetByEmailAsync(email);

            if (user == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromUserAccount(user));
        }
        [HttpGet]
        public async Task<JsonResult> GetOrdersAsync(int id)
        {
            UserAccount? user = await _uow.Users.GetAsync(id);

            if (user == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromUserAccount(user)!.Orders);
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<UserAccountDto> dtos = new List<UserAccountDto>();
            List<UserAccount> users = await _uow.Users.GetAllAsync();

            foreach (UserAccount user in users)
            {
                dtos.Add(Map.FromUserAccount(user)!);
            }

            return new JsonResult(dtos);
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _uow.Users.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}