using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.Model;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_WebAPI.Mapper;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IngredientAmountController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<IngredientAmountController> _logger;

        public IngredientAmountController(IUnitOfWork uow, ILogger<IngredientAmountController> logger)
        {
            _uow = uow;
            _logger = logger;
        }
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] IngredientDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _uow.IngredientAmounts.CreateAsync(Map.ToIngredientAmount(dto)!);
            else
            {
                bool success = await _uow.IngredientAmounts.UpdateAsync(Map.ToIngredientAmount(dto)!);

                if (!success) return new JsonResult(NotFound());
            }

            await _uow.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            IngredientAmount? ia = await _uow.IngredientAmounts.GetAsync(id);

            if (ia == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromIngredientAmount(ia));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<IngredientDto> dtos = new List<IngredientDto>();
            List<IngredientAmount> ingredientAmounts = await _uow.IngredientAmounts.GetAllAsync();

            foreach (IngredientAmount ingredientAmount in ingredientAmounts)
            {
                dtos.Add(Map.FromIngredientAmount(ingredientAmount)!);
            }

            return new JsonResult(dtos);
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _uow.IngredientAmounts.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}
