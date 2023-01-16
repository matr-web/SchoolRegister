using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.Models.Dto_s.SubjectDto_s;
using SchoolRegister.Models.Dto_s.SubjectDto_sl;
using SchoolRegister.Utility;

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
    [Authorize]
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
    [Authorize]
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
    [Authorize(Roles = StaticData.role_administrator)]
    public async Task<ActionResult> PostAsync([FromBody] CreateSubjectDto createSubjectDto)
    {
        var subjectDto = await _subjectService.InsertSubjectAsync(createSubjectDto);

        return Created($"Subject/{subjectDto.Id}", subjectDto);
    }

    [HttpPut("subjectId")]
    [Authorize(Roles = StaticData.role_administrator)]
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
    [Authorize(Roles = StaticData.role_administrator)]
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
