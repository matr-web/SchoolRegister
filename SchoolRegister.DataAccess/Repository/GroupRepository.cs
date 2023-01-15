using Microsoft.EntityFrameworkCore;
using SchoolRegister.DataAccess;

namespace SchoolRegister.DataAcces.Repository;

public class GroupRepository : Repository<GroupEntity>, IGroupRepository
{
    private readonly SchoolRegisterContext _context;

    public GroupRepository(SchoolRegisterContext context) : base(context)
    {
        _context = context;
    }

    public new Task Remove(GroupEntity group)
    {
        var _group = _context.Groups
            .Include(g => g.Students)
            .ThenInclude(s => s.Grades)
            .FirstOrDefault(g => g.Id == group.Id);

        _context.Remove(_group);
        return Task.CompletedTask;
    }
}
