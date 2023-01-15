using SchoolRegister.DataAccess;

namespace SchoolRegister.DataAcces.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolRegisterContext context;

    public UnitOfWork(SchoolRegisterContext context)
    {
        this.context = context;

        GradeRepository = new GradeRepository(context);
        GroupRepository = new GroupRepository(context);
        RoleRepository = new RoleRepository(context);
        SubjectRepository = new SubjectRepository(context);
        UserRepository = new UserRepository(context);
    }

    public IGradeRepository GradeRepository { get; private set; }
    public IGroupRepository GroupRepository { get; private set; }
    public IRoleRepository RoleRepository { get; private set; }
    public ISubjectRepository SubjectRepository { get; private set; }
    public IUserRepository UserRepository { get; private set; }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
