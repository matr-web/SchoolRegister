using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.DataAcces.Repository.IRepository;
using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.GroupDto_s;
using System.Linq.Expressions;

namespace SchoolRegister.BusinessAccess.Services;


public class GroupService : IGroupService
{
    private readonly IUnitOfWork _unitOfWork;

    public GroupService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<GroupDto>> GetGroupsAsync(Expression<Func<GroupEntity, bool>> filterExpression = null, string includeProperties = null)
    {
        var groups = await _unitOfWork.GroupRepository.GetAllAsync(filterExpression, includeProperties);

        var groupDtos = new List<GroupDto>();

        foreach (var group in groups)
        {
            var groupDto = GroupDto.ToGroupDtoMap(group);
            groupDtos.Add(groupDto);
        }

        return groupDtos;
    }

    public async Task<GroupDto> GetGroupByAsync(Expression<Func<GroupEntity, bool>> filterExpression = null, string includeProperties = null)
    {
        var group = await _unitOfWork.GroupRepository.GetByAsync(filterExpression, includeProperties);

        var groupDto = GroupDto.ToGroupDtoMap(group);

        return groupDto;
    }

    public async Task<GroupDto> InsertGroupAsync(CreateGroupDto createGroupDto)
    {
        var group = new GroupEntity()
        {
            Name = createGroupDto.Name
        };

        await _unitOfWork.GroupRepository.AddAsync(group);
        await _unitOfWork.SaveAsync();

        var groupDto = GroupDto.ToGroupDtoMap(group);

        return groupDto;
    }

    public async Task<GroupDto> UpdateGroupAsync(UpdateGroupDto updateGroupDto)
    {
        var group = await _unitOfWork.GroupRepository.GetByAsync(g => g.Id == updateGroupDto.Id, "Students,Subjects");

        group.Name = updateGroupDto.Name;

        await _unitOfWork.GroupRepository.UpdateAsync(group);
        await _unitOfWork.SaveAsync();

        var groupDto = GroupDto.ToGroupDtoMap(group);

        return groupDto;
    }

    public async Task DeleteGroupAsync(GroupDto groupDto)
    {
        var group = await _unitOfWork.GroupRepository.GetByAsync(g => g.Id == groupDto.Id);

        if (group == null)
        {
            throw new ArgumentException();
        }

        await _unitOfWork.GroupRepository.Remove(group);
        await _unitOfWork.SaveAsync();
    }
}
