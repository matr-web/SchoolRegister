using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.GroupDto_s;
using System.Linq.Expressions;

namespace SchoolRegister.BusinessAccess.Interfaces;

public interface IGroupService
{
    Task<IEnumerable<GroupDto>> GetGroupsAsync(Expression<Func<GroupEntity, bool>> filterExpression = null, string includeProperties = null);
    Task<GroupDto> GetGroupByAsync(Expression<Func<GroupEntity, bool>> filterExpression = null, string includeProperties = null);
    Task<GroupDto> InsertGroupAsync(CreateGroupDto createGroupDto);
    Task<GroupDto> UpdateGroupAsync(UpdateGroupDto updateGroupDto);
    Task DeleteGroupAsync(GroupDto groupDto);
}
