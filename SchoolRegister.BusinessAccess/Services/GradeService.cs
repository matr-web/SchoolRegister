using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.DataAcces.Repository.IRepository;
using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.GradeDto_s;
using System.Linq.Expressions;

namespace SchoolRegister.BusinessAccess.Services;

public class GradeService : IGradeService
{
    private readonly IUnitOfWork _unitOfWork;

    public GradeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<GradeDto>> GetGradesAsync(Expression<Func<GradeEntity, bool>> filterExpression = null, string includeProperties = null)
    {
        var grades = await _unitOfWork.GradeRepository.GetAllAsync(filterExpression, includeProperties);

        var gradeDtos = new List<GradeDto>();

        foreach (var grade in grades)
        {
            var gradeDto = GradeDto.ToGradeDtoMap(grade);
            gradeDtos.Add(gradeDto);
        }

        return gradeDtos;
    }

    public async Task<GradeDto> GetGradeByAsync(Expression<Func<GradeEntity, bool>> filterExpression = null, string includeProperties = null)
    {
        var grade = await _unitOfWork.GradeRepository.GetByAsync(filterExpression, includeProperties);

        var gradeDto = GradeDto.ToGradeDtoMap(grade);

        return gradeDto;
    }

    public async Task<GradeDto> InsertGradeAsync(CreateGradeDto createGradeDto)
    {
        var grade = new GradeEntity()
        {
            DateOfIssue = DateTime.Now,
            GradeValue = createGradeDto.GradeValue,
            UserId = createGradeDto.StudentId,
            SubjectId = createGradeDto.SubjectId,
        };

        await _unitOfWork.GradeRepository.AddAsync(grade);
        await _unitOfWork.SaveAsync();

        var gradeDto = GradeDto.ToGradeDtoMap(grade);

        return gradeDto;
    }

    public async Task<GradeDto> UpdateGradeAsync(UpdateGradeDto updateGradeDto)
    {
        var grade = await _unitOfWork.GradeRepository.GetByAsync(g => g.Id == updateGradeDto.Id, "Student,Subject");

        grade.DateOfIssue= DateTime.Now;
        grade.GradeValue = updateGradeDto.GradeValue;

        await _unitOfWork.GradeRepository.UpdateAsync(grade);
        await _unitOfWork.SaveAsync();

        var gradeDto = GradeDto.ToGradeDtoMap(grade);

        return gradeDto;
    }

    public async Task DeleteGradeAsync(GradeDto gradeDto)
    {
        var grade = await _unitOfWork.GradeRepository.GetByAsync(g => g.Id == gradeDto.Id, "Student,Subject");

        if (grade == null)
        {
            throw new ArgumentException();
        }

        await _unitOfWork.GradeRepository.Remove(grade);
        await _unitOfWork.SaveAsync();
    }
}
