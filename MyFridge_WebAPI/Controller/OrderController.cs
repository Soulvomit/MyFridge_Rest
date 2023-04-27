using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.DataModel;
using MyFridge_Library_MAUI_Client.ClientModel;
using MyFridge_WebAPI.Service.Mapper.Interface;
using MyFridge_WebAPI.Service.Data.Interface;

namespace MyFridge_WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IMapperService _map;
        private readonly ILogger<OrderController> _log;

        public OrderController(IDataService dataService, IMapperService map, ILogger<OrderController> log)
        {
            _dataService = dataService;
            _map = map;
            _log = log;
        }
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] OrderDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _dataService.Orders.CreateAsync(_map.ToOrder(from: dto));
            else
            {
                bool success = await _dataService.Orders.UpdateAsync(_map.ToOrder(from: dto));

                if (!success) return new JsonResult(NotFound());
            }

            await _dataService.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            Order? order = await _dataService.Orders.GetAsync(id);

            if (order == null) return new JsonResult(NotFound());

            return new JsonResult(_map.ToOrderDto(from: order));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<OrderDto> dtos = new List<OrderDto>();
            List<Order> orders = await _dataService.Orders.GetAllAsync();

            foreach (Order order in orders)
            {
                dtos.Add(_map.ToOrderDto(from: order));
            }

            return new JsonResult(dtos);
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _dataService.Orders.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _dataService.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}
