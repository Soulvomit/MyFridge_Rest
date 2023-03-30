using Microsoft.AspNetCore.Mvc;
using MyFridge_Library_Data.Model;
using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_WebAPI.Mapper;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipyController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<RecipyController> _logger;
        public RecipyController(IUnitOfWork uow, ILogger<RecipyController> logger)
        {
            _uow = uow;
            _logger = logger;
        }
        [HttpPost]
        public async Task<JsonResult> UpsertAsync([FromBody] RecipyDto dto)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());

            if (dto.Id == 0) await _uow.Recipies.CreateAsync(Map.ToRecipy(dto));
            else
            {
                bool success = await _uow.Recipies.UpdateAsync(Map.ToRecipy(dto));

                if (!success) return new JsonResult(NotFound());
            }

            await _uow.CompleteAsync();

            return new JsonResult(dto);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsync(int id)
        {
            Recipy? recipy = await _uow.Recipies.GetAsync(id);

            if (recipy == null) return new JsonResult(NotFound());

            return new JsonResult(Map.FromRecipy(recipy));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync()
        {
            List<RecipyDto> dtos = new List<RecipyDto>();
            List<Recipy> recipies = await _uow.Recipies.GetAllAsync();

            foreach (Recipy recipy in recipies)
            {
                dtos.Add(Map.FromRecipy(recipy));
            }

            return new JsonResult(dtos);
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            bool success = await _uow.Recipies.DeleteAsync(id);

            if (!success) return new JsonResult(NotFound());

            await _uow.CompleteAsync();

            return new JsonResult(NoContent());
        }
    }
}