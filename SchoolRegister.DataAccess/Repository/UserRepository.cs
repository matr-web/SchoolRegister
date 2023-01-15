using SchoolRegister.DataAccess;

namespace SchoolRegister.DataAcces.Repository;

public class UserRepository : Repository<UserEntity>, IUserRepository
{
    public UserRepository(SchoolRegisterContext context) : base(context)
    {
        
    }
}
