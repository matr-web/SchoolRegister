namespace SchoolRegister.DataAcces.Repository.IRepository;

public interface IUnitOfWork
{
    IGradeRepository GradeRepository { get; }
    IGroupRepository GroupRepository { get; }
    IRoleRepository RoleRepository { get; }
    ISubjectRepository SubjectRepository { get; }
    IUserRepository UserRepository { get; }
    Task SaveAsync();
}
