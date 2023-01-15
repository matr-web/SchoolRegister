using SchoolRegister.DataAccess;

namespace SchoolRegister.DataAcces.Repository;

public class GroupRepository : Repository<GroupEntity>, IGroupRepository
{
    public GroupRepository(SchoolRegisterContext context) : base(context)
    {
      
    }   
}
