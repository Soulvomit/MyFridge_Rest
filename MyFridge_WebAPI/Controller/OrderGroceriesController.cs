using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.DataModel;
using MyFridge_Library_MAUI_Client.ClientModel;
using MyFridge_WebAPI.Service.Mapper.Interface;
using MyFridge_WebAPI.Service.Data.Interface;

namespace MyFridge_WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderGroceriesController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IMapperService _map;
        private readonly ILogger<OrderController> _log;

        public OrderGroceriesController(IDataService dataService, IMapperService map, ILogger<OrderController> log)
        {
            _dataService = dataService;
            _map = map;
            _log = log;
        }
        [HttpPost]
        public async Task<JsonResult> UpsertAsync(int id, [FromBody] GroceryDto dto)
        {
            if(dto == null) return new JsonResult(NotFound());

            Order? order = await _dataService.Orders.GetAsync(id);
            if (order == null) return new JsonResult(NotFound());

            await _dataService.Orders.AddGroceryAsync(order.Id, _map.ToGrocery(from: dto));

            await _dataService.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id, int groceryId)
        {
            Order? order = await _dataService.Orders.GetAsync(id);
            if (order == null) return new JsonResult(NotFound());

            Grocery? grocery = order.Groceries.FirstOrDefault(g => g.Id == groceryId);
            if (grocery == null) return new JsonResult(NotFound());

            return new JsonResult(_map.ToGroceryDto(from: grocery));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync(int id)
        {
            Order? order = await _dataService.Orders.GetAsync(id);
            if (order == null) return new JsonResult(NotFound());

            return new JsonResult(_map.ToOrderDto(from: order).Groceries);
        }
        //delete
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id, int groceryId)
        {
            Order? order = await _dataService.Orders.GetAsync(id);
            if (order == null) return new JsonResult(NotFound());

            bool success = await _dataService.Orders.RemoveGroceryAsync(order.Id, groceryId);

            if (!success) return new JsonResult(NotFound());

            await _dataService.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}
