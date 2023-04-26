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
        public async Task<JsonResult> UpsertAsync(int id, [FromBody] IngredientAmountDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            bool success = await _uow.Users.AddIngredientAmountAsync(id, Map.ToIngredientAmount(dto));

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id, int ingredientAmountId)
        {
            UserAccount? user = await _uow.Users.GetAsync(id);

            if (user == null) return new JsonResult(NotFound());

            IngredientAmount? ingredientAmount = user.IngredientAmounts
                .FirstOrDefault(ingredientAmount => ingredientAmount.Id == ingredientAmountId);
            
            if (ingredientAmount == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromIngredientAmount(ingredientAmount));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync(int id)
        {
            UserAccount? user = await _uow.Users.GetAsync(id);

            if (user == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromUserAccount(user).Ingredients);
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id, int ingredientAmountId, float removeAmount, bool forceRemove = true)
        {
            bool success = 
                await _uow.Users.RemoveAmountAsync(id, ingredientAmountId, removeAmount, forceRemove);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
} 