using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.Model;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_WebAPI.Mapper;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroceryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<GroceryController> _logger;
        public GroceryController(IUnitOfWork uow, ILogger<GroceryController> logger)
        {
            _uow = uow;  
            _logger = logger;
        }
        //create/edit
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] GroceryDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _uow.Groceries.CreateAsync(Map.ToGrocery(dto)!);
            else
            {
                bool success = await _uow.Groceries.UpdateAsync(Map.ToGrocery(dto)!);

                if (!success) return new JsonResult(NotFound());
            }

            await _uow.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            Grocery? grocery = await _uow.Groceries.GetAsync(id);

            if (grocery == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromGrocery(grocery));
        }
        [HttpGet]
        public async Task<JsonResult> GetFilteredAsync(string filter, int minLength = 2)
        {
            Func<Grocery, bool> filterFunc = grocery =>
                grocery.IngredientAmount.Ingredient.Name.ToLower().Contains(filter.ToLower()) ||
                grocery.Brand.ToLower().Contains(filter.ToLower());

            Func<Grocery, object> orderByFunc = grocery => grocery.IngredientAmount.Ingredient.Name;

            List<GroceryDto> dtos = new();
            List<Grocery>? filteredGroceries = await _uow.Groceries.Query(filterFunc, orderByFunc, filter, minLength);

            if (filteredGroceries == null) return new JsonResult(NotFound());

            foreach (Grocery grocery in filteredGroceries)
            {
                dtos.Add(Map.FromGrocery(grocery)!);
            }
            return new JsonResult(dtos);
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<GroceryDto> dtos = new List<GroceryDto>();
            List<Grocery> groceries = await _uow.Groceries.GetAllAsync();

            foreach (Grocery grocery in groceries)
            {
                dtos.Add(Map.FromGrocery(grocery)!);
            }

            return new JsonResult((dtos));
        }
        //delete
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _uow.Groceries.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}