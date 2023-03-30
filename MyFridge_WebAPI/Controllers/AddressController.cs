using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.Model;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_WebAPI.Mapper;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IUnitOfWork uow, ILogger<AddressController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        //create/edit
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] AddressDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _uow.Addresses.CreateAsync(Map.ToAddress(dto)!);
            else
            {
                bool success = await _uow.Addresses.UpdateAsync(Map.ToAddress(dto)!);

                if (!success) return new JsonResult(NotFound());
            }

            await _uow.CompleteAsync();

            return new JsonResult(dto);
        }
        //get
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            Address? address = await _uow.Addresses.GetAsync(id);

            if (address == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromAddress(address));
        }
        //get all
        [HttpGet]
        public async Task<JsonResult> GetAllAsync() 
        {
            List<AddressDto> dtos = new List<AddressDto>();
            List<Address> addresses = await _uow.Addresses.GetAllAsync();

            foreach(Address address in addresses)
            {
                dtos.Add(Map.FromAddress(address)!);
            }

            return new JsonResult(dtos);
        }
        //delete
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _uow.Addresses.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}
