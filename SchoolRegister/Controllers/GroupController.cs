using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.Models.Dto_s.GroupDto_s;
using SchoolRegister.Utility;

namespace SchoolRegister.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet("GetAll")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GroupDto>>> GetAllAsync()
    {
        var groupDtos = await _groupService.GetGroupsAsync(includeProperties: "Students,Subjects");

        if (groupDtos == null)
        {
            return NotFound();
        }

        return Ok(groupDtos);
    }

    [HttpGet("groupId")]
    [Authorize]
    public async Task<ActionResult<GroupDto>> GetAsync([FromQuery] int groupId)
    {
        var groupDto = await _groupService.GetGroupByAsync(includeProperties: "Students,Subjects");

        if (groupDto == null)
        {
            return NotFound();
        }

        return Ok(groupDto);
    }

    [HttpPost]
    [Authorize(Roles = StaticData.role_administrator)]
    public async Task<ActionResult> PostAsync([FromBody] CreateGroupDto createGroupDto)
    {
        var groupDto = await _groupService.InsertGroupAsync(createGroupDto);

        return Created($"Group/{groupDto.Id}", groupDto);
    }

    [HttpPut("groupId")]
    [Authorize(Roles = StaticData.role_administrator)]
    public async Task<ActionResult> PutAsync([FromQuery] int groupId, [FromBody] UpdateGroupDto updateGroupDto)
    {
        if (groupId != updateGroupDto.Id)
        {
            return BadRequest();
        }

        var groupDto = await _groupService.UpdateGroupAsync(updateGroupDto);

        return Ok(groupDto);
    }

    [HttpDelete("groupId")]
    [Authorize(Roles = StaticData.role_administrator)]
    public async Task<ActionResult> DeleteAsync([FromQuery] int groupId)
    {
        var groupDto = await _groupService.GetGroupByAsync(g => g.Id == groupId);

        if (groupDto == null)
        {
            return NotFound();
        }

        await _groupService.DeleteGroupAsync(groupDto);

        return NoContent();
    }
}
