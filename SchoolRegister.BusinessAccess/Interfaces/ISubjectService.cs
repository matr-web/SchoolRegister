using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.SubjectDto_s;
using SchoolRegister.Models.Dto_s.SubjectDto_sl;
using System.Linq.Expressions;

namespace SchoolRegister.BusinessAccess.Interfaces;

public interface ISubjectService
{
    Task<IEnumerable<SubjectDto>> GetSubjectsAsync(Expression<Func<SubjectEntity, bool>> filterExpression = null, string includeProperties = null);
    Task<SubjectDto> GetSubjectByAsync(Expression<Func<SubjectEntity, bool>> filterExpression, string includeProperties = null);
    Task<SubjectDto> InsertSubjectAsync(CreateSubjectDto createSubjectDto);
    Task<SubjectDto> UpdateSubjectAsync(UpdateSubjectDto updateSubjectDto);
    Task DeleteSubjectAsync(SubjectDto subjectDto);
}
