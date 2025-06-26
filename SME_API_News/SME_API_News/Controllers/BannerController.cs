using Microsoft.AspNetCore.Mvc;
using SME_API_News.Entities;
using SME_API_News.Models;

using SME_API_News.Repository;

namespace SME_API_News.Controllers
{
    [ApiController]
    [Route("api/SME_NEWS_V1/[controller]")]
    public class BannerController : ControllerBase
    {
        private readonly IBannerRepository _repo;
        private readonly IWebHostEnvironment _env;

        public BannerController(IBannerRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repo.GetAllAsync());

        [HttpGet("active")]
        public async Task<IActionResult> GetActive() =>
            Ok(await _repo.GetActiveSortedAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var banner = await _repo.GetByIdAsync(id);
            return banner == null ? NotFound() : Ok(banner);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if (file.Length == 0) return BadRequest("File is empty");

            var uploads = Path.Combine(_env.WebRootPath, "uploads/banners");
            Directory.CreateDirectory(uploads);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploads, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"/uploads/banners/{fileName}";
            return Ok(new { imageUrl });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] MBanner banner)
        {
            try
            {
                if (banner.Id == 0)
                {
                    await _repo.AddAsync(banner);
                    return Ok(banner);
                }
                else
                {

                    await _repo.UpdateAsync(banner);
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MBanner banner)
        {
            if (id != banner.Id) return BadRequest();
            await _repo.UpdateAsync(banner);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
          var xresult =  await _repo.DeleteAsync(id);
            return xresult;
        }

        [HttpPost]
        [Route("GetBanner")]
        public async Task<ActionResult<ViewBannerNewsModels>> GetBanner([FromBody] BannerModels models)
        {
            ViewBannerNewsModels vm = new ViewBannerNewsModels();
            var categories = await _repo.GetBanner(models);
            if (categories.listBannerModels.Count == 0)
            {
                return NotFound("No data found.");
            }
            vm.listBannerModels = categories.listBannerModels;
            vm.SearchBannerModels = models;
            vm.TotalRowsList = categories.TotalRowsList;
            return Ok(vm);
        }
    }


}
