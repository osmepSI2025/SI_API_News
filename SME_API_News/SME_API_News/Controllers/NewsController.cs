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
                CreateBy = "System", // Replace with actual user context
                CreateDate = DateTime.UtcNow,
                IsPin = Mnews.IsPin,
                BusinessUnitId = Mnews.BusinessUnitId,
                CoverFilePath = Mnews.CoverFilePath,
                PicNewsFilePath = Mnews.PicNewsFilePath,
                NewsFilePath = Mnews.NewsFilePath
                ,OrderId = Mnews.OrderId
            };


            await _repository.UpdateAsync(news);
            return NoContent();
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
        //[HttpPost("{newsId}/tags/{tagId}")]
        //public async Task<ActionResult> AddTag(int newsId, int tagId)
        //{
        //    await _newsService.AddTagToNewsAsync(newsId, tagId);
        //    return NoContent();
        //}

        //[HttpDelete("{newsId}/tags/{tagId}")]
        //public async Task<ActionResult> RemoveTag(int newsId, int tagId)
        //{
        //    await _newsService.RemoveTagFromNewsAsync(newsId, tagId);
        //    return NoContent();
        //}
    }
}
