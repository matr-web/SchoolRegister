using SchoolRegister.DataAccess;

namespace SchoolRegister.DataAcces.Repository;

public class GradeRepository : Repository<GradeEntity>, IGradeRepository
{
    public GradeRepository(SchoolRegisterContext context) : base(context)
    {
        
    }
}
