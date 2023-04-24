using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.Model;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_WebAPI.Mapper;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<RecipeController> _logger;
        public RecipeController(IUnitOfWork uow, ILogger<RecipeController> logger)
        {
            _uow = uow;
            _logger = logger;
        }
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] RecipeDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _uow.Recipes.CreateAsync(Map.ToRecipe(dto));
            else
            {
                bool success = await _uow.Recipes.UpdateAsync(Map.ToRecipe(dto));

                if (!success) return new JsonResult(NotFound());
            }

            await _uow.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            Recipe? recipe = await _uow.Recipes.GetAsync(id);

            if (recipe == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromRecipe(recipe));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<RecipeDto> dtos = new List<RecipeDto>();
            List<Recipe> recipes = await _uow.Recipes.GetAllAsync();

            foreach (Recipe recipe in recipes)
            {
                dtos.Add(Map.FromRecipe(recipe));
            }

            return new JsonResult(dtos);
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _uow.Recipes.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}