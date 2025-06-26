using Microsoft.AspNetCore.Mvc;
using SME_API_News.Entities;
using SME_API_News.Models;
using SME_API_News.Repository;

[Route("api/SME_NEWS_V1/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _repository;

    public CategoriesController(ICategoryRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    [Route("GetCategory")]
    public async Task<ActionResult<ViewCategoryNewsModels>> GetCategory(CategoryNewsModels models)
    {
        ViewCategoryNewsModels vm = new ViewCategoryNewsModels();
       var categories = await _repository.GetAllAsync(models);
        if ( categories.listMCategoryModels.Count == 0)
        {
            return NotFound("No categories found.");
        }
        vm.listMCategoryModels = categories.listMCategoryModels;
        vm.SearchCategoryNewsModels = models;
        vm.TotalRowsList = categories.TotalRowsList;
        return Ok(vm);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryNewsModels>> GetById(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        return category == null ? NotFound() : Ok(category);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] MCategory category)
    {
        try
        {
            if (category.Id==0) 
            {
                await _repository.AddAsync(category);
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
            } else 
            {
              
                await _repository.UpdateAsync(category);
                return NoContent();
            }
           
        }
        catch (Exception ex) 
        {
        return StatusCode(500, $"Internal server error: {ex.Message}");
        }
     
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] MCategory category)
    {
        if (id != category.Id) return BadRequest();
        await _repository.UpdateAsync(category);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
