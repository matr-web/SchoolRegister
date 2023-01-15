using SchoolRegister.DataAccess;

namespace SchoolRegister.DataAcces.Repository;

public class SubjectRepository : Repository<SubjectEntity>, ISubjectRepository
{
	public SubjectRepository(SchoolRegisterContext context) : base(context)
	{

	}
}
