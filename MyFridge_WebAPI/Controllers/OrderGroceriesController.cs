using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.Model;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_WebAPI.Mapper;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderGroceriesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<OrderController> _logger;

        public OrderGroceriesController(IUnitOfWork uow, ILogger<OrderController> logger)
        {
            _uow = uow;
            _logger = logger;
        }
        [HttpPost]
        public async Task<JsonResult> UpsertAsync(int id, [FromBody] GroceryDto dto)
        {
            if(dto == null) return new JsonResult(NotFound());

            Order? order = await _uow.Orders.GetAsync(id);
            if (order == null) return new JsonResult(NotFound());

            await _uow.Orders.AddGroceryAsync(order.Id, Map.ToGrocery(dto));

            await _uow.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id, int groceryId)
        {
            Order? order = await _uow.Orders.GetAsync(id);
            if (order == null) return new JsonResult(NotFound());

            Grocery? grocery = order.Groceries.FirstOrDefault(g => g.Id == groceryId);
            if (grocery == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromGrocery(grocery));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync(int id)
        {
            Order? order = await _uow.Orders.GetAsync(id);
            if (order == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromOrder(order).Groceries);
        }
        //delete
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id, int groceryId)
        {
            Order? order = await _uow.Orders.GetAsync(id);
            if (order == null) return new JsonResult(NotFound());

            bool success = await _uow.Orders.RemoveGroceryAsync(order.Id, groceryId);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}
