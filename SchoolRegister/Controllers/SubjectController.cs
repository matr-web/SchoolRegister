using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.Models.Dto_s.SubjectDto_s;
using SchoolRegister.Models.Dto_s.SubjectDto_sl;

namespace SchoolRegister.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<SubjectDto>>> GetAllAsync()
    {
        var subjectDtos = await _subjectService.GetSubjectsAsync(includeProperties: "Teacher");

        if (subjectDtos == null)
        {
            return NotFound();
        }

        return Ok(subjectDtos);
    }

    [HttpGet("subjectId")]
    public async Task<ActionResult<SubjectDto>> GetAsync([FromQuery] int subjectId)
    {
        var subjectDto = await _subjectService.GetSubjectByAsync(includeProperties: "Teacher");

        if (subjectDto == null)
        {
            return NotFound();
        }

        return Ok(subjectDto);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] CreateSubjectDto createSubjectDto)
    {
        var subjectDto = await _subjectService.InsertSubjectAsync(createSubjectDto);

        return Created($"Subject/{subjectDto.Id}", subjectDto);
    }

    [HttpPut("subjectId")]
    public async Task<ActionResult> PutAsync([FromQuery] int subjectId, [FromBody] UpdateSubjectDto updateSubjectDto)
    {
        if (subjectId != updateSubjectDto.Id)
        {
            return BadRequest();
        }

        var subjectDto = await _subjectService.UpdateSubjectAsync(updateSubjectDto);

        return Ok(subjectDto);
    }

    [HttpDelete("subjectId")]
    public async Task<ActionResult> DeleteAsync([FromQuery] int subjectId)
    {
        var subjectDto = await _subjectService.GetSubjectByAsync(g => g.Id == subjectId);

        if (subjectDto == null)
        {
            return NotFound();
        }

        await _subjectService.DeleteSubjectAsync(subjectDto);

        return NoContent();
    }
}
