using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.Model;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_WebAPI.Mapper;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipyIngredientsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<RecipyController> _logger;
        public RecipyIngredientsController(IUnitOfWork uow, ILogger<RecipyController> logger)
        {
            _uow = uow;
            _logger = logger;
        }
        [HttpPost]
        public async Task<JsonResult> UpsertAsync(int id, [FromBody]IngredientAmountDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            bool success = await _uow.Recipies.AddIngredientAsync(id, Map.ToIngredientAmount(dto)!);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id, int iaId)
        {
            Recipy? recipy = await _uow.Recipies.GetAsync(id);
            if (recipy == null) return new JsonResult(NotFound());

            IngredientAmount? ia = recipy.IngredientAmounts.FirstOrDefault(ia => ia.Id == iaId);
            if (ia == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromIngredientAmount(ia));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync(int id)
        {
            Recipy? recipy = await _uow.Recipies.GetAsync(id);
            if (recipy == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromRecipy(recipy).Ingredients);
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id, int iaId)
        {
            bool success = await _uow.Recipies.RemoveIngredientAsync(id, iaId);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}