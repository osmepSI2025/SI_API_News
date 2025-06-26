using Microsoft.AspNetCore.Mvc;
using SME_API_News.Entities;
using SME_API_News.Models;
using SME_API_News.Repository;

[Route("api/SME_NEWS_V1/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentRepository _repository;

    public DepartmentController(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MDepartmentModels>>> GetAll()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MDepartmentModels>> GetById(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        return category == null ? NotFound() : Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] MDepartment param)
    {
        await _repository.AddAsync(param);
        return CreatedAtAction(nameof(GetById), new { id = param.Id }, param);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] MDepartment param)
    {
        if (id != param.Id) return BadRequest();
        await _repository.UpdateAsync(param);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
