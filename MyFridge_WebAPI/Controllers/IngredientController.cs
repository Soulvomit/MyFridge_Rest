using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.Model;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_WebAPI.Mapper;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<IngredientController> _logger;

        public IngredientController(IUnitOfWork uow, ILogger<IngredientController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        //create/edit
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] IngredientDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _uow.Ingredients.CreateAsync(Map.ToIngredient(dto)!);
            else
            {
                bool success = await _uow.Ingredients.UpdateAsync(Map.ToIngredient(dto)!);

                if (!success) return new JsonResult(NotFound());
            }

            await _uow.CompleteAsync();

            return new JsonResult((dto));
        }
        //get
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            Ingredient? ingredient = await _uow.Ingredients.GetAsync(id);

            if (ingredient == null) return new JsonResult(NotFound());

            return new JsonResult((Map.FromIngredient(ingredient)));
        }
        //get all
        [HttpGet()]
        public async Task<JsonResult> GetAllAsync()
        {
            List<IngredientDto> dtos = new List<IngredientDto>();
            List<Ingredient> ingredients = await _uow.Ingredients.GetAllAsync();

            foreach (Ingredient ingredient in ingredients)
            {
                dtos.Add(Map.FromIngredient(ingredient)!);
            }

            return new JsonResult((dtos));
        }
        //delete
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _uow.Ingredients.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}
