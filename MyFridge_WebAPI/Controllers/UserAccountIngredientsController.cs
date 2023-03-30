using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.Model;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_WebAPI.Mapper;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAccountIngredientsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UserAccountController> _logger;

        public UserAccountIngredientsController(IUnitOfWork uow, ILogger<UserAccountController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        [HttpPost]
        public async Task<JsonResult> UpsertAsync(int id, [FromBody] IngredientDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            bool success = await _uow.Users.AddIngredientAsync(id, Map.ToIngredient(dto), dto.Amount);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id, int iaId)
        {
            UserAccount? user = await _uow.Users.GetAsync(id);
            if (user == null) return new JsonResult(NotFound());

            IngredientAmount? ia = user.IngredientAmounts.FirstOrDefault(ia => ia.Id == iaId);
            if (ia == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromIngredientAmount(ia));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync(int id)
        {
            UserAccount? user = await _uow.Users.GetAsync(id);

            if (user == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromUserAccount(user).Ingredients);
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id, int iaId, float removeAmount, 
            bool forceRemove = true)
        {
            bool success = 
                await _uow.Users.RemoveIngredientAmountAsync(id, iaId, removeAmount, forceRemove);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
} 