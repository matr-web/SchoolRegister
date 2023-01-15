using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.DataAcces.Repository.IRepository;
using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.SubjectDto_s;
using SchoolRegister.Models.Dto_s.SubjectDto_sl;
using System.Linq.Expressions;

namespace SchoolRegister.BusinessAccess.Services;

public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork;

    public SubjectService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SubjectDto>> GetSubjectsAsync(Expression<Func<SubjectEntity, bool>> filterExpression = null, string includeProperties = null)
    {
        var subjects = await _unitOfWork.SubjectRepository.GetAllAsync(filterExpression, includeProperties);

        var subjectDtosList = new List<SubjectDto>();

        foreach (var subject in subjects)
        {
            var subjectDto = SubjectDto.ToSubjectDtoMap(subject);
            subjectDtosList.Add(subjectDto);
        }

        return subjectDtosList;
    }

    public async Task<SubjectDto> GetSubjectByAsync(Expression<Func<SubjectEntity, bool>> filterExpression = null, string includeProperties = null)
    {
        var subject = await _unitOfWork.SubjectRepository.GetByAsync(filterExpression, includeProperties);

        var subjectDto = SubjectDto.ToSubjectDtoMap(subject);

        return subjectDto;
    }

    public async Task<SubjectDto> InsertSubjectAsync(CreateSubjectDto createSubjectDto)
    {
        var subject = new SubjectEntity()
        {
            Name = createSubjectDto.Name,
            Description = createSubjectDto.Description,
            TeacherId = createSubjectDto.TeacherId
        };

        await _unitOfWork.SubjectRepository.AddAsync(subject);
        await _unitOfWork.SaveAsync();

        var subjectDto = SubjectDto.ToSubjectDtoMap(subject);

        return subjectDto;
    }

    public async Task<SubjectDto> UpdateSubjectAsync(UpdateSubjectDto updateSubjectDto)
    {
        var subject = await _unitOfWork.SubjectRepository.GetByAsync(s => s.Id == updateSubjectDto.Id, "Teacher");

        subject.Id = updateSubjectDto.Id;
        subject.Name = updateSubjectDto.Name;
        subject.Description = updateSubjectDto.Description;
        subject.TeacherId = updateSubjectDto.TeacherId;

        await _unitOfWork.SubjectRepository.UpdateAsync(subject);
        await _unitOfWork.SaveAsync();

        var subjectDto = SubjectDto.ToSubjectDtoMap(subject);

        return subjectDto;
    }

    public async Task DeleteSubjectAsync(SubjectDto subjectDto)
    {
        var subject = await _unitOfWork.SubjectRepository.GetByAsync(s => s.Id == subjectDto.Id);

        if (subject == null)
        {
            throw new ArgumentException();
        }

        await _unitOfWork.SubjectRepository.Remove(subject);
        await _unitOfWork.SaveAsync();
    }
}
