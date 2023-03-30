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
        //get all
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
    }
}
