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
        [HttpPost]
        public async Task<JsonResult> AddIngredientAsync([FromBody] IngredientDto dto, int id)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            bool success = await _uow.Users.AddIngredientAsync(id, Map.ToIngredient(dto)!, dto.Amount);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(dto);

        }
        //get
        [HttpGet]
        public async Task<JsonResult> GetUserAccountAsync(int id)
        {
            UserAccount? user = await _uow.Users.GetAsync(id);

            if (user == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromUserAccount(user));
        }
        [HttpGet]
        public async Task<JsonResult> GetUserAccountByEmailAsync(string email)
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
        public async Task<JsonResult> ShowRecipiesAsync(int id)
        {
            return new JsonResult(await _uow.Users.GetValidRicipiesAsync(id));
        }
        [HttpGet]
        public async Task<JsonResult> GetIngredientsAsync(int id)
        {
            UserAccount? user = await _uow.Users.GetAsync(id);

            if (user == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromUserAccount(user)!.Ingredients);
        }
        [HttpGet]
        public async Task<JsonResult> GetAddressAsync(int id)
        {
            UserAccount? user = await _uow.Users.GetAsync(id);

            if (user == null) return new JsonResult(NotFound());
            if (user.Address == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromAddress(user.Address));
        }
        //get all
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
        //delete
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _uow.Users.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
        [HttpDelete]
        public async Task<JsonResult> RemoveIngredientAsync(int id, int index)
        {
            bool success = await _uow.Users.RemoveIngredientAsync(id, index);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
        [HttpDelete]
        public async Task<JsonResult> RemoveAmountAsync(int id, [FromBody]IngredientDto updateEntity, float removeAmount, bool forceRemove = true)
        {
            bool success = await _uow.Users.RemoveAmountAsync(id, Map.ToIngredient(updateEntity), removeAmount, forceRemove);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
        [HttpDelete]
        public async Task<JsonResult> MakeRecipyAsync(int id, [FromBody] RecipyDto updateEntity, bool force = false)
        {
            bool success = await _uow.Users.MakeRicipiesAsync(id, Map.ToRecipy(updateEntity), force);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}