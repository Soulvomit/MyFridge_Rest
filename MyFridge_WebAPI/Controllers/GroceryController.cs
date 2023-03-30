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

            return new JsonResult((dto));
        }
        [HttpPost]
        public async Task<JsonResult> ChangeIngredientAsync([FromBody] IngredientDto dto, int id)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            bool success = await _uow.Groceries.ChangeIngredientAsync(id, Map.ToIngredient(dto)!, dto.Amount);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult((dto));
        }
        //get
        [HttpGet]
        public async Task<JsonResult> GetGroceryAsync(int id)
        {
            Grocery? grocery = await _uow.Groceries.GetAsync(id);

            if (grocery == null) return new JsonResult(NotFound());

            return new JsonResult((Map.FromGrocery(grocery)));
        }
        [HttpGet]
        public async Task<JsonResult> GetIngredientAsync(int id)
        {
            Grocery? grocery = await _uow.Groceries.GetAsync(id);

            if (grocery == null) return new JsonResult(NotFound());
            if (grocery.IngredientAmount == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromIngredientAmount(grocery.IngredientAmount));
        }
        //get all
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