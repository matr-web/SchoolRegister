using SchoolRegister.DataAccess;

namespace SchoolRegister.DataAcces.Repository;

public class RoleRepository : Repository<RoleEntity>, IRoleRepository
{
    public RoleRepository(SchoolRegisterContext context) : base(context)
    {

    }
}