using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.DataModel;
using MyFridge_Library_MAUI_Client.ClientModel;
using MyFridge_WebAPI.Service.Mapper.Interface;
using MyFridge_WebAPI.Service.Data.Interface;

namespace MyFridge_WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IMapperService _map;
        private readonly ILogger<RecipeController> _log;
        public RecipeController(IDataService dataService, IMapperService map, ILogger<RecipeController> log)
        {
            _dataService = dataService;
            _map = map;
            _log = log;
        }
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] RecipeDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _dataService.Recipes.CreateAsync(_map.ToRecipe(from: dto));
            else
            {
                bool success = await _dataService.Recipes.UpdateAsync(_map.ToRecipe(from: dto));

                if (!success) return new JsonResult(NotFound());
            }

            await _dataService.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            Recipe? recipe = await _dataService.Recipes.GetAsync(id);

            if (recipe == null) return new JsonResult(NotFound());

            return new JsonResult(_map.ToRecipeDto(from: recipe));
        }
        [HttpGet]
        public async Task<JsonResult> GetFilteredAsync(string filter, int minLength = 2)
        {
            Func<Recipe, bool> filterFunc = recipe =>
                recipe.Name.ToLower().Contains(filter.ToLower()) ||
                recipe.IngredientAmounts.Any(ia => 
                ia.Ingredient.Name.ToLower().Contains(filter.ToLower()));

            Func<Recipe, object> orderByFunc = recipe => recipe.Name;

            List<RecipeDto> dtos = new();
            IEnumerable<Recipe>? filteredRecipes = 
                await _dataService.Recipes.Query(filterFunc, orderByFunc, filter, minLength);

            if (filteredRecipes == null) return new JsonResult(NotFound());

            foreach (Recipe recipe in filteredRecipes)
            {
                dtos.Add(_map.ToRecipeDto(from: recipe));
            }
            return new JsonResult(dtos);
        }
        [HttpGet]
        public async Task<JsonResult> GetMakeableAsync(int userId)
        {
            UserAccount user = await _dataService.Users.GetAsync(userId);
            
            if (user == null) return new JsonResult(NotFound());

            Func<Recipe, bool> filterFunc = recipe => 
                recipe.IngredientAmounts.All(recipeIngredient => 
                    user.IngredientAmounts.Any(userIngredient => 
                        recipeIngredient.Ingredient.Id == userIngredient.Ingredient.Id &&
                        recipeIngredient.Amount <= userIngredient.Amount));

            Func<Recipe, object> orderByFunc = recipe => recipe.Name;

            List<RecipeDto> dtos = new();
            IEnumerable<Recipe>? makeableRecipes = await _dataService.Recipes.Query(filterFunc, orderByFunc);

            if (makeableRecipes == null) return new JsonResult(NotFound());

            foreach (Recipe recipe in makeableRecipes)
            {
                dtos.Add(_map.ToRecipeDto(from: recipe));
            }
            return new JsonResult(dtos);
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<RecipeDto> dtos = new List<RecipeDto>();
            List<Recipe> recipes = await _dataService.Recipes.GetAllAsync();

            foreach (Recipe recipe in recipes)
            {
                dtos.Add(_map.ToRecipeDto(from: recipe));
            }

            return new JsonResult(dtos);
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _dataService.Recipes.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _dataService.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}