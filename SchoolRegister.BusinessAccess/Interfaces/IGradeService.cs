using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.GradeDto_s;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.BusinessAccess.Interfaces;

public interface IGradeService
{
    Task<IEnumerable<GradeDto>> GetGradesAsync(Expression<Func<GradeEntity, bool>> filterExpression = null, string includeProperties = null);
    Task<GradeDto> GetGradeByAsync(Expression<Func<GradeEntity, bool>> filterExpression = null, string includeProperties = null);
    Task<GradeDto> InsertGradeAsync(CreateGradeDto createGradeDto);
    Task<GradeDto> UpdateGradeAsync(UpdateGradeDto updateGradeDto);
    Task DeleteGradeAsync(GradeDto gradeDto);
}
