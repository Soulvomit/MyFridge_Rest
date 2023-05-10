using Client_Model.Model;
using Data_Interface.Service.Data.Interface;
using Data_Interface.Service.Mapper.Interface;
using Data_Model.Model;
using Microsoft.AspNetCore.Mvc;

namespace Data_Interface.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IngredientAmountController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IMapperService _map;
        private readonly ILogger<IngredientAmountController> _log;

        public IngredientAmountController(IDataService dataService, IMapperService map, ILogger<IngredientAmountController> log)
        {
            _dataService = dataService;
            _map = map;
            _log = log;
        }
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] IngredientAmountCto cto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (cto.Id == 0) await _dataService.IngredientAmounts.CreateAsync(_map.ToIngredientAmountDto(from: cto));
            else
            {
                bool success = await _dataService.IngredientAmounts.UpdateAsync(_map.ToIngredientAmountDto(from: cto));

                if (!success) return new JsonResult(NotFound());
            }

            await _dataService.CompleteAsync();

            return new JsonResult(cto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            IngredientAmountDto? ingredientAmount = await _dataService.IngredientAmounts.GetAsync(id);

            if (ingredientAmount == null) return new JsonResult(NotFound());

            return new JsonResult(_map.ToIngredientAmountCto(from: ingredientAmount));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<IngredientAmountCto> dtos = new List<IngredientAmountCto>();
            List<IngredientAmountDto> ingredientAmounts = await _dataService.IngredientAmounts.GetAllAsync();

            foreach (IngredientAmountDto ingredientAmount in ingredientAmounts)
            {
                dtos.Add(_map.ToIngredientAmountCto(from: ingredientAmount));
            }

            return new JsonResult(dtos);
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _dataService.IngredientAmounts.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _dataService.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}
