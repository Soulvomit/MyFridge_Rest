using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.Model;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_WebAPI.Mapper;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IUnitOfWork uow, ILogger<OrderController> logger)
        {
            _uow = uow;
            _logger = logger;
        }
        //create/edit
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] OrderDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _uow.Orders.CreateAsync(Map.ToOrder(dto)!);
            else
            {
                bool success = await _uow.Orders.UpdateAsync(Map.ToOrder(dto)!);

                if (!success) return new JsonResult(NotFound());
            }

            await _uow.CompleteAsync();

            return new JsonResult(dto);
        }
        //get
        [HttpGet]
        public async Task<JsonResult> GetOrderAsync(int id)
        {
            Order? order = await _uow.Orders.GetAsync(id);

            if (order == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromOrder(order));
        }
        //get
        [HttpGet]
        public async Task<JsonResult> GetGroceriesAsync(int id)
        {
            Order? order = await _uow.Orders.GetAsync(id);

            if (order == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromOrder(order)!.Groceries);
        }
        //get all
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<OrderDto> dtos = new List<OrderDto>();
            List<Order> orders = await _uow.Orders.GetAllAsync();

            foreach (Order order in orders)
            {
                dtos.Add(Map.FromOrder(order)!);
            }

            return new JsonResult(dtos);
        }
        //delete
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _uow.Orders.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}
