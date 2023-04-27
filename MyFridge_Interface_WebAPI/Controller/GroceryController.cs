using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.DataModel;
using MyFridge_Library_Client_MAUI.ClientModel;
using MyFridge_Interface_WebAPI.Service.Mapper.Interface;
using MyFridge_Interface_WebAPI.Service.Data.Interface;

namespace MyFridge_Interface_WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroceryController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IMapperService _map;
        private readonly ILogger<GroceryController> _log;
        public GroceryController(IDataService dataService, IMapperService map, ILogger<GroceryController> log)
        {
            _dataService = dataService;  
            _map = map;
            _log = log;
        }
        //create/edit
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] GroceryDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _dataService.Groceries.CreateAsync(_map.ToGrocery(from: dto));
            else
            {
                bool success = await _dataService.Groceries.UpdateAsync(_map.ToGrocery(from: dto));

                if (!success) return new JsonResult(NotFound());
            }

            await _dataService.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            Grocery? grocery = await _dataService.Groceries.GetAsync(id);

            if (grocery == null) return new JsonResult(NotFound());

            return new JsonResult(_map.ToGroceryDto(from: grocery));
        }
        [HttpGet]
        public async Task<JsonResult> GetFilteredAsync(string filter, int minLength = 2)
        {
            Func<Grocery, bool> filterFunc = grocery =>
                grocery.IngredientAmount.Ingredient.Name.ToLower().Contains(filter.ToLower()) ||
                grocery.Brand.ToLower().Contains(filter.ToLower());

            Func<Grocery, object> orderByFunc = grocery => grocery.IngredientAmount.Ingredient.Name;

            List<GroceryDto> dtos = new();
            List<Grocery>? filteredGroceries = await _dataService.Groceries.Query(filterFunc, orderByFunc, filter, minLength);

            if (filteredGroceries == null) return new JsonResult(NotFound());

            foreach (Grocery grocery in filteredGroceries)
            {
                dtos.Add(_map.ToGroceryDto(from: grocery));
            }
            return new JsonResult(dtos);
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<GroceryDto> dtos = new List<GroceryDto>();
            List<Grocery> groceries = await _dataService.Groceries.GetAllAsync();

            foreach (Grocery grocery in groceries)
            {
                dtos.Add(_map.ToGroceryDto(from: grocery));
            }

            return new JsonResult((dtos));
        }
        //delete
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _dataService.Groceries.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _dataService.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}