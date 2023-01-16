using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.Models.Dto_s.GradeDto_s;
using SchoolRegister.Utility;
using System.Data;

namespace SchoolRegister.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradeController : ControllerBase
{
    private readonly IGradeService _gradeService;

    public GradeController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpGet("GetAll")]
    [Authorize(Roles = $"{StaticData.role_teacher},{StaticData.role_student}")]
    public async Task<ActionResult<IEnumerable<GradeDto>>> GetAllAsync([FromQuery] Guid studentId)
    {
        var gradeDtos = await _gradeService.GetGradesAsync(g => g.UserId == studentId, "Student,Subject");

        if (gradeDtos == null)
        {
            return NotFound();
        }

        return Ok(gradeDtos);
    }

    [HttpGet("gradeId")]
    [Authorize(Roles = $"{StaticData.role_teacher},{StaticData.role_student}")]
    public async Task<ActionResult<GradeDto>> GetAsync([FromQuery] int gradeId)
    {
        var gradeDto = await _gradeService.GetGradeByAsync(g => g.Id == gradeId, "Student,Subject");

        if (gradeDto == null)
        {
            return NotFound();
        }

        return Ok(gradeDto);
    }

    [HttpPost]
    [Authorize(Roles = StaticData.role_teacher)]
    public async Task<ActionResult> PostAsync([FromBody] CreateGradeDto createGradeDto)
    {
        var gradeDto = await _gradeService.InsertGradeAsync(createGradeDto);

        return Created($"Grade/{gradeDto.Id}", gradeDto);
    }

    [HttpPut("gradeId")]
    [Authorize(Roles = StaticData.role_teacher)]
    public async Task<ActionResult> PutAsync([FromQuery] int gradeId, [FromBody] UpdateGradeDto updateGradeDto)
    {
        if (gradeId != updateGradeDto.Id)
        {
            return BadRequest();
        }

        var gradeDto = await _gradeService.UpdateGradeAsync(updateGradeDto);

        return Ok(gradeDto);
    }

    [HttpDelete("gradeId")]
    [Authorize(Roles = StaticData.role_teacher)]
    public async Task<ActionResult> DeleteAsync([FromQuery] int gradeId)
    {
        var gradeDto = await _gradeService.GetGradeByAsync(g => g.Id == gradeId);

        if (gradeDto == null)
        {
            return NotFound();
        }

        await _gradeService.DeleteGradeAsync(gradeDto);

        return NoContent();
    }
}
