using Microsoft.AspNetCore.Mvc;
using SME_API_News.Entities;
using SME_API_News.Models;
using SME_API_News.Repository;

namespace SME_API_News.Controllers
{
    [ApiController]
    [Route("api/SME_NEWS_V1/[controller]")]
    public class MPopupController : ControllerBase
    {
        private readonly IMPopupRepository _repository;

        public MPopupController(IMPopupRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActivePopups()
        {
            var entities = await _repository.GetActivePopupsAsync();
            var models = entities.Select(MapToModel);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(MapToModel(entity));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PopupModels model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var entity = MapToEntity(model);
                    await _repository.AddAsync(entity);
                    return CreatedAtAction(nameof(GetById), new { id = entity.Id }, MapToModel(entity));

                }
                else
                {
                    
                    var entity = MapToEntity(model);
                    await _repository.UpdateAsync(entity);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PopupModels model)
        {
            if (id != model.Id) return BadRequest();
            var entity = MapToEntity(model);
            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
          var xresult=  await _repository.DeleteAsync(id);
            return xresult;
        }

        // Mapping
        private static PopupModels MapToModel(MPopup e) => new()
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            ImageUrl = e.ImageUrl,
            StartDateTime = e.StartDateTime,
            EndDateTime = e.EndDateTime,
            FlagActive = e.FlagActive,
            CreateDate = e.CreateDate,
            UpdateDate = e.UpdateDate,
            CreateBy = e.CreateBy,
            UpdateBy = e.UpdateBy
        };

        private static MPopup MapToEntity(PopupModels m) => new()
        {
            Id = m.Id,
            Title = m.Title,
            Description = m.Description,
            ImageUrl = m.ImageUrl,
            StartDateTime = m.StartDateTime,
            EndDateTime = m.EndDateTime,
            FlagActive = m.FlagActive,
            CreateDate = m.CreateDate,
            UpdateDate = m.UpdateDate,
            CreateBy = m.CreateBy,
            UpdateBy = m.UpdateBy
        };

        [HttpPost]
        [Route("GetPopup")]
        public async Task<ActionResult<ViewPopupModels>> GetPopup([FromBody] PopupModels models)
        {
            ViewPopupModels vm = new ViewPopupModels();
            var ldata = await _repository.GetPopup(models);
            if (ldata.listPopupModels.Count == 0)
            {
              //  return NotFound("No data found.");
                return Ok(vm);
            }
            vm.listPopupModels = ldata.listPopupModels;
            vm.SearchPopupModels = models;
            vm.TotalRowsList = ldata.TotalRowsList;
            return Ok(vm);
        }
    }


}
