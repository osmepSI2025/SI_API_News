using Microsoft.AspNetCore.Mvc;
using SME_API_News.Entities;
using SME_API_News.Models;
using SME_API_News.Repository;

namespace SME_API_News.Controllers
{
    [ApiController]
    [Route("api/SME_NEWS_V1/[controller]")]
    // [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _repository;

        public NewsController(INewsRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("GetNews")]
        [ProducesResponseType(typeof(ViewMNewsModels), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetNews([FromBody] MNewsModels param)
        {
            if (param == null)
            {
                return BadRequest("Invalid parameter.");
            }

            try
            {

                ViewMNewsModels result = new ViewMNewsModels();
                
                result = await _repository.GetAllAsync(param);



                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception (เช่นใช้ ILogger)
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MNewsModels>> GetNews(int id)
        {
            var news = await _repository.GetByIdAsync(id);

            if (news == null)
            {
                return NotFound();
            }
            return Ok(news);
        }

        [HttpPost]
        [Route("CreateNews")]
        public async Task<ActionResult<MNewsModels>> PostNews(MNewsModels news)
        {
            int resut = await _repository.AddAsyncNews(news);
            news.Id = resut; // Set the generated id to news.Id

            return CreatedAtAction(nameof(GetNews), new { id = resut }, news);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNews(int id, MNewsModels Mnews)
        {
            if (id != Mnews.Id)
            {
                return BadRequest();
            }

            if (!await _repository.ExistsAsync(id))
            {
                return NotFound();
            }
            var news = new MNews
            {
                Id = Mnews.Id,
                ArticlesContent = Mnews.ArticlesContent,
                ArticlesShortDescription = Mnews.ArticlesShortDescription,
                ArticlesTitle = Mnews.ArticlesTitle,
                ArticlesAutherName = Mnews.ArticlesAutherName,
                CatagoryCode = Mnews.CatagoryCode,
                PublishDate = Mnews.PublishDate,
                StartDate = Mnews.StartDate,
                EndDate = Mnews.EndDate,
                IsPublished = Mnews.IsPublished,             
            
                IsPin = Mnews.IsPin,
                BusinessUnitId = Mnews.BusinessUnitId,
                CoverFilePath = Mnews.CoverFilePath,
                PicNewsFilePath = Mnews.PicNewsFilePath,
                NewsFilePath = Mnews.NewsFilePath
                ,OrderId = Mnews.OrderId
                ,FileNameOriginal = Mnews.FileNameOriginal,
                UpdateBy = Mnews.UpdateBy

            };


            await _repository.UpdateAsync(news);

            var newsReturn = await _repository.GetByIdAsync(Mnews.Id);
            return Ok(newsReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            if (!await _repository.ExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Route("UpdateRangeNews")]
        [ProducesResponseType(typeof(ViewMNewsModels), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateRangeNews([FromBody] MNewsModels param)
        {
            if (param == null)
            {
                return BadRequest("Invalid parameter.");
            }

            try
            {

                MNews xdata = new MNews();
                xdata.Id = param.Id;
                xdata.OrderId = param.OrderId;
                 await _repository.UpdateRangeAsync(xdata);



                return Ok(1);
            }
            catch (Exception ex)
            {
                return Ok(0);
                // Log the exception (เช่นใช้ ILogger)
              
            }
        }

        [HttpPost]
        [Route("UpdateStatusActiveNews")]
        [ProducesResponseType(typeof(ViewMNewsModels), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public  async Task<bool> UpdateStatusActiveNews([FromBody] MNewsModels param)
        {
            try 
            {
                  if (param == null || param.Id <= 0)
                {
                    return false;
                }
                  await _repository.UpdateStatusActiveNews(param);
                return true;
            }
            catch
            (Exception ex) 
            {
                return false;
            }
            // Example for EF Core, adjust for your data access method
           
        }
    }
}
